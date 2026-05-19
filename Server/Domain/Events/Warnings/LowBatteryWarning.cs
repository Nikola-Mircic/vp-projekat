using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Events
{
    public class LowBatteryWarning : Warning
    {
        public LowBatteryWarning(string participantId, EegSample sample) :
            base($"Low battery warning for participant {participantId} at row {sample.RowIndex}", sample)
        {}
    }
}
