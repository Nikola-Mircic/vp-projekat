using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class ContactValidator : IValidator<int, int>
    {
        private int _min_contact;

        public ContactValidator(int min_contact)
        {
            _min_contact = min_contact;
        }

        public ValidationResult<int> Check(int value)
        {
            bool valid = value >= _min_contact;
            return new ValidationResult<int>(valid, value);
        }
    }
}
