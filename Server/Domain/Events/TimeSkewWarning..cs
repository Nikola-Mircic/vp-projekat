using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Events
{
    internal class TimeSkewWarning : Warning
    {
        public TimeSkewWarning(string participantId, TimeSpan skew, EegSample sample) :
            base($"Time skew warning for participant {participantId} at row {sample.RowIndex}: Skew of {skew.TotalSeconds:F2} seconds detected", sample)
        { }
    }
}
