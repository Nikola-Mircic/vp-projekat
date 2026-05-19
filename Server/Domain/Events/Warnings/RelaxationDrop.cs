using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Events
{
    internal class RelaxationDrop : Warning
    {
        public RelaxationDrop(string participantId,int previousValue, EegSample sample) :
            base($"[{sample.Timestamp:G}]Relaxation drop detected for participant {participantId} at row {sample.RowIndex}: {previousValue} -> {sample.Relaxation}", sample)
        {}
    }
}
