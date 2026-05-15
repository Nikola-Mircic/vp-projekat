using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class EegEvents
    {
        // Define events for session management and data reception
        public static event Action<string> OnTransferStarted;
        public static event Action<EegSample> OnSampleReceived;
        public static event Action<string> OnTransferCompleted;

        // Validation events
        public static event Action<Warning> OnWarningRaised;

        // Session management event delegates
        public delegate void TransferStartedHandler(string participantId);
        public delegate void TransferCompletedHandler(string participantId);
        public delegate void SampleReceivedHandler(EegSample sample);

        public delegate void ValidationWarningHandler(Warning warning);

        public static void RaiseTransferStarted(string participantId)
        {
            OnTransferStarted?.Invoke(participantId);
        }

        public static void RaiseSampleReceived(EegSample sample)
        {
            OnSampleReceived?.Invoke(sample);
        }

        public static void RaiseTransferCompleted(string participantId)
        {
            OnTransferCompleted?.Invoke(participantId);
        }

        public static void RaiseWarning(Warning warning)
        {
            OnWarningRaised?.Invoke(warning);
        }
    }
}
