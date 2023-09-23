using Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Infra.Data.Repository
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordOptions _passwordOptions;
        public PasswordHasher(IOptions<PasswordOptions> passwordOptions)
        {
            _passwordOptions = passwordOptions.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpect hash Format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorith = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    iterations
                ))
            {
                var keyToCheck = algorith.GetBytes(_passwordOptions.KeySize);

                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using (var algorith = new Rfc2898DeriveBytes(
                    password,
                    _passwordOptions.SaltSize,
                    _passwordOptions.Iterations
                ))
            {
                var key = Convert.ToBase64String(algorith.GetBytes(_passwordOptions.KeySize));
                var salt = Convert.ToBase64String(algorith.Salt);

                return $"{_passwordOptions.Iterations}.{salt}.{key}";
            }
        }
    }
}
