using Common;
using Common.Faults;
using Common.Response;
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

        public Server()
        {
            sessionStorage = new FileSessionStorage();

            InitializeValidators();
            InitializeEventHandlers();
        }

        public ServiceResponse StartSession(EegMeta meta)
        {
            this.sessionMetadata = meta;
            sessionStorage.StartSession(meta);

            EegEvents.RaiseTransferStarted(meta.ParticipantId);

            return new ServiceResponse()
            {
                Acknowledgement = Acknowledgement.ACK,
                Status = Status.COMPLETED
            };
        }

        public ServiceResponse EndSession()
        {
            EegEvents.RaiseTransferCompleted(sessionMetadata.ParticipantId);
            sessionStorage.EndSession();

            return new ServiceResponse()
            {
                Acknowledgement = Acknowledgement.ACK,
                Status = Status.COMPLETED
            };
        }

        public ServiceResponse PushSample(string sample)
        {
            EegSample eegSample;
            
            try {
                eegSample = ValidateFormat(sample);
            }
            catch (Exception ex)
            {
                throw CreateDataFormatFault(ex.Message);
            }

            EegEvents.RaiseSampleReceived(eegSample);

            string validationMessage = ValidateSample(eegSample);

            if (validationMessage == "")
            {
                bool result = sessionStorage.PushSample(eegSample);

                if (result)
                {
                    return new ServiceResponse()
                    {
                        Acknowledgement = Acknowledgement.ACK,
                        Status = eegSample.RowIndex == sessionMetadata.TotalRows ? Status.COMPLETED : Status.IN_PROGRESS
                    };
                }
                else
                {
                    return new ServiceResponse()
                    {
                        Acknowledgement = Acknowledgement.NACK,
                        Status = eegSample.RowIndex == sessionMetadata.TotalRows ? Status.COMPLETED : Status.IN_PROGRESS
                    };
                }
            }
            else
            {
                EegEvents.RaiseWarning(new Warning(validationMessage, eegSample));
                throw CreateValidationFault(validationMessage);
            }
        }

        private EegSample ValidateFormat(string sample)
        {
            ValidationResult<EegSample> result = dataFormatValidator.Check(sample);
            return result.Value;
        }

        private string ValidateSample(EegSample sample)
        {
            if (!batteryValidator.Check(sample.Battery).Valid)
                return "Battery level too low";

            if (!contactValidator.Check(sample.ContactQuality).Valid)
                return "Contact quality too low";

            if (!relaxationValidator.Check(sample.Relaxation).Valid)
                return "Relaxation level dropped too much";

            if (!rowIndexValidator.Check(sample.RowIndex).Valid)
                return "Row index out of order";

            if (!stressSpikeValidator.Check(sample.Stress).Valid)
                return "Stress spike detected";

            if (!timeSkewValidator.Check(sample.Timestamp).Valid)
                return "Timestamp skew detected";

            return "";
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
            EegEvents.OnSampleReceived += (sample) => Console.WriteLine($"Sample received: {sample.ToCsv()}");
            EegEvents.OnTransferCompleted += (participantId) => Console.WriteLine($"Session completed for participant: {participantId}");

            EegEvents.OnWarningRaised += (warning) =>
            {
                if (sessionStorage != null)
                    sessionStorage.PushWarning(warning);
                Console.WriteLine($"Warning raised: {warning.Message}");
            };
        }

        private FaultException<ValidationFault> CreateValidationFault(string message)
        {
            return new FaultException<ValidationFault>(
                    new ValidationFault() { Message = message },
                    new FaultReason(message)
                );
        }

        private FaultException<DataFormatFault> CreateDataFormatFault(string message)
        {
            return new FaultException<DataFormatFault>(
                    new DataFormatFault() { Message = message },
                    new FaultReason(message)
                );
        }
    }
}
