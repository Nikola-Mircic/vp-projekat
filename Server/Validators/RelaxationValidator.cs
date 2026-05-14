using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class RelaxationValidator : IValidator<double, double>
    {
        private double _max_relaxation;
        private double? _relaxation;

        public RelaxationValidator(double max_relaxation)
        {
            _max_relaxation = max_relaxation;
        }

        public ValidationResult<double> Check(double value)
        {
            if (_relaxation == null) {
               _relaxation = value;
               return new ValidationResult<double>(true, value);
            }

            double d = value - _relaxation.Value;
            _relaxation = value;

            if(Math.Abs(d) <= _max_relaxation)
                return new ValidationResult<double>(true, d);

            return new ValidationResult<double>(false, d);
        }
    }
}
