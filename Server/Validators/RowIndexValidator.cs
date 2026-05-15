using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class RowIndexValidator : IValidator<int, int>
    {
        private int? _last_index;

        public RowIndexValidator() { }

        public ValidationResult<int> Check(int value)
        {
            if (_last_index == null)
            {
                _last_index = value;
                return new ValidationResult<int>(true, value);
            }

            bool valid = value == _last_index + 1;
            _last_index = value;

            return new ValidationResult<int>(valid, value);
        }
    }
}
