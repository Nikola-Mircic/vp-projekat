using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Events
{
    public class PoorContactWarning : Warning
    {
        public PoorContactWarning(string participantId, EegSample sample) :
            base($"Poor contact warning for participant {participantId} at row {sample.RowIndex}", sample)
        { }
    }
}
