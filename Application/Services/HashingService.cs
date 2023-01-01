using Application.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HashingService: IHashingService
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        readonly byte[] salt;
        

        public HashingService(IConfiguration configuration)
        {
            salt = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Salt"));
        }

        public string HashPassword(string password)
        {

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
                );

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }

    }
}
