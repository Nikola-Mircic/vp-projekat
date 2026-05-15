using Common;
using Common.Faults;
using Server.Config;
using Server.Data;
using Server.Domain;
using Server.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server : IService
    {
        EegMeta sessionMetadata;
        ISessionStorage sessionStorage;

        DataFormatValidator dataFormatValidator;

        BatteryValidator batteryValidator;
        ContactValidator contactValidator;
        RelaxationValidator relaxationValidator;
        RowIndexValidator rowIndexValidator;
        StressSpikeValidator stressSpikeValidator;
        TimeSkewValidator timeSkewValidator;

        public Server() {
            sessionStorage = new FileSessionStorage();

            InitializeValidators();
            InitializeEventHandlers();
        }

        public bool EndSession()
        {
            EegEvents.RaiseTransferCompleted(sessionMetadata.ParticipantId);
            sessionStorage.EndSession();
            return true;
        }

        public bool PushSample(string sample)
        {
            EegSample eegSample = ValidateFormat(sample);

            ValidateSample(eegSample);

            EegEvents.RaiseSampleReceived(eegSample);

            return sessionStorage.PushSample(eegSample);
        }

        public bool StartSession(EegMeta meta)
        {
            this.sessionMetadata = meta;
            sessionStorage.StartSession(meta);

            EegEvents.RaiseTransferStarted(meta.ParticipantId);

            return true;
        }

        private EegSample ValidateFormat(string sample)
        {
            try
            {
                ValidationResult<EegSample> result = dataFormatValidator.Check(sample);
                return result.Value;
            }
            catch (Exception ex)
            {
                throw new FaultException<DataFormatFault>(
                     new DataFormatFault() { Message = ex.Message }
                    );
            }
        }

        private void ValidateSample(EegSample sample)
        {

            if(!batteryValidator.Check(sample.Battery).Valid)
                throw new FaultException<ValidationFault>(
                    new ValidationFault() { Message = "Battery level too low" }
                );

            if(!contactValidator.Check(sample.ContactQuality).Valid)
                throw new FaultException<ValidationFault>(
                    new ValidationFault() { Message = "Contact quality too low" }
                );

            if(!relaxationValidator.Check(sample.Relaxation).Valid)
                throw new FaultException<ValidationFault>(
                    new ValidationFault() { Message = "Relaxation level dropped too much" }
                );

            if(!rowIndexValidator.Check(sample.RowIndex).Valid)
                throw new FaultException<ValidationFault>(
                    new ValidationFault() { Message = "Row index is not sequential" }
                );

            if(!stressSpikeValidator.Check(sample.Stress).Valid)
                throw new FaultException<ValidationFault>(
                    new ValidationFault() { Message = "Stress level spiked too much" }
                );

            if(!timeSkewValidator.Check(sample.Timestamp).Valid)
                throw new FaultException<ValidationFault>(
                    new ValidationFault() { Message = "Timestamp skew is too high" }
                );
        }

        private void InitializeValidators()
        {
            dataFormatValidator = new DataFormatValidator();
            batteryValidator = new BatteryValidator(AppConfig.BatteryLowThreshold);
            contactValidator = new ContactValidator(AppConfig.ContactQualityMin);
            relaxationValidator = new RelaxationValidator(AppConfig.RelaxationDropThreshold);
            rowIndexValidator = new RowIndexValidator();
            stressSpikeValidator = new StressSpikeValidator(AppConfig.StressSpikeThreshold);
            timeSkewValidator = new TimeSkewValidator(TimeSpan.FromMilliseconds(AppConfig.TimestampSkewMaxMs));
        }

        private void InitializeEventHandlers()
        {
            EegEvents.OnTransferStarted += (participantId) => Console.WriteLine($"Session started for participant: {participantId}");
            EegEvents.OnSampleReceived += (sample) => Console.WriteLine($"Sample received: {sample}");
            EegEvents.OnTransferCompleted += (participantId) => Console.WriteLine($"Session completed for participant: {participantId}");

            EegEvents.OnWarningRaised += (warning) =>
            {
                if (sessionStorage != null)
                    sessionStorage.PushWarning(warning);
                Console.WriteLine($"Warning raised: {warning.Message}");
            };
        }
    }
}
