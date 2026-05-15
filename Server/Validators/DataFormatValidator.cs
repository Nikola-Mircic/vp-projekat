using Common;
using Server.Domain;
using Server.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class DataFormatValidator : IValidator<string, EegSample>
    {
        public ValidationResult<EegSample> Check(string value)
        {
            EegSample result = EegSampleParser.TryParse(value);
            return new ValidationResult<EegSample>(result != null, result);
        }
    }
}
