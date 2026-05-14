using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public interface IValidator <V, R>
    {
        ValidationResult<R> Check(V value);
    }
}
