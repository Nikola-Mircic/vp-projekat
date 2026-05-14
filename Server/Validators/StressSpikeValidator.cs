using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class StressSpikeValidator : IValidator<double, double>
    {
        private double? _value;
        private double _max_spike;

        public StressSpikeValidator(double maxSpike)
        {
            _max_spike = maxSpike;
        }

        public ValidationResult<double> Check(double value)
        {
            if(_value == null)
            {
                _value = value;
                return new ValidationResult<double>(true, value);
            }

            double d = value - _value.Value;
            _value = value;

            if (Math.Abs(d) <= _max_spike)
                return new ValidationResult<double> (true, d);

            return new ValidationResult<double>(false, d);
        }
    }
}
