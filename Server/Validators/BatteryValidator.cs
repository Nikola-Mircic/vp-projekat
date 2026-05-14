using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class BatteryValidator : IValidator<int, int>
    {
        private int _min_battery;

        public BatteryValidator(int min_battery)
        {
            _min_battery = min_battery;
        }

        public ValidationResult<int> Check(int value)
        {
            bool valid = value >= _min_battery;
            return new ValidationResult<int>(valid, value);
        }
    }
}
