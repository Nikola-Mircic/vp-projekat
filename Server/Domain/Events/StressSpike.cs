using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Events
{
    public class StressSpike : Warning
    {
        public StressSpike(string participantId, int dSpike, EegSample sample) : 
            base($"Stress spike detected for participant {participantId}: {(dSpike > 0 ? '+':'-')}{dSpike}", sample)
        {}
    }
}
