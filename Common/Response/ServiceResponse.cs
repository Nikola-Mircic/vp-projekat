using Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Response
{
    [DataContract]
    public class ServiceResponse
    {
        [DataMember]
        public Acknowledgement Acknowledgement { get; set; }

        [DataMember]
        public Status Status { get; set; }
    }
}
