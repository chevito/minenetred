using System;

namespace Minenetred.web.Infrastructure
{
    public interface IEncryptionService
    {
        String Encrypt(string encryptString);

        string Decrypt(string cipherText);
    }
}