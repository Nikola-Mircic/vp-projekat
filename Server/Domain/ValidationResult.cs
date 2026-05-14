using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class ValidationResult<T>
    {
        public bool Valid { get; private set; }

        public T Value { get; private set; }

        public ValidationResult(bool valid, T value)
        {
            Valid = valid;
            Value = value;
        }
    }
}
