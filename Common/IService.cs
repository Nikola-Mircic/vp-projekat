using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        ServiceResponse StartSession(EegMeta meta);

        [OperationContract]
        ServiceResponse PushSample(EegSample sample);

        [OperationContract]
        ServiceResponse EndSession();
    }
}
