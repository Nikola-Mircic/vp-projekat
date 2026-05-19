using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class Warning
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }

        public EegSample Sample { get; set; }

        public Warning(string message, EegSample sample)
        {
            TimeStamp = DateTime.Now;
            Message = message;
            Sample = sample;
        }

        public string ToCsv()
        {
            return $"{TimeStamp:G},{Message},{Sample.ToCsv()}";
        }
    }
}
