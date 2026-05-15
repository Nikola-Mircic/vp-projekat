using Common.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool StartSession(EegMeta meta);

        [OperationContract]
        [FaultContract(typeof(DataFormatFault))]
        [FaultContract(typeof(ValidationFault))]
        bool PushSample(string sample);

        [OperationContract]
        bool EndSession();
    }
}
