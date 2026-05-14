using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    internal class Events
    {
        // Define events for session management and data reception
        public event Action<string> OnTransferStarted;
        public event Action<EegSample> OnSampleReceived;
        public event Action<string> OnTransferCompleted;

        // Validation events
        public event Action<Warning> OnWarningRaised;

        // Session management event delegates
        public delegate void SessionStartedHandler(string participantId);
        public delegate void SessionCompletedHandler(string participantId);
        public delegate void SampleReceivedHandler(EegSample sample);

        public delegate void ValidationWarningHandler(Warning warning);
    }
}
