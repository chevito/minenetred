using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.web.Infrastructure
{
    public interface IHashHelper
    {
        byte[] GetHash(string inputString);
        string GetHashString(string inputString);

    }
}
