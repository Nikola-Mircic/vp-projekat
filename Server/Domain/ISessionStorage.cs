using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    internal interface ISessionStorage : IDisposable
    {
        bool StartSession(EegMeta meta);
        bool PushSample(EegSample sample);
    }
}
