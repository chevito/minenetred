using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.web.Infrastructure
{
    public interface IEncryptionHelper
    {
        String encrypt(string encryptString);
        string Decrypt(string cipherText);

    }
}
