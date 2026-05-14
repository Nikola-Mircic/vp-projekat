using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class TimeSkewValidator : IValidator<DateTime, TimeSpan>
    {
        private DateTime? _last;
        private TimeSpan _defaultTimeSkew;

        public TimeSkewValidator(TimeSpan defaultTimeSkew)
        {
            _defaultTimeSkew = defaultTimeSkew;
        }

        public ValidationResult<TimeSpan> Check(DateTime value)
        {
            if (_last == null) { 
                _last = value;
                return new ValidationResult<TimeSpan>(true, _defaultTimeSkew);
            }

            TimeSpan skew = value - _last.Value;
            _last = value;

            bool valid = skew < _defaultTimeSkew;
            return new ValidationResult<TimeSpan>(valid, skew);
        }
    }
}
