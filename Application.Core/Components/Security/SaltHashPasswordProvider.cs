namespace Application.Components.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using Microsoft.Extensions.Options;

    public class SaltHashPasswordProvider : IPasswordProvider
    {
        private readonly Random random = new Random();

        private readonly SaltHashPasswordOptions options;

        public SaltHashPasswordProvider(IOptions<SaltHashPasswordOptions> options)
        {
            this.options = options.Value;
        }

        public bool Match(string password, string hash)
        {
            var salt = hash.Substring(0, options.SaltLength);

            using (var algorithm = SHA256.Create())
            {
                var bytes = algorithm.ComputeHash(Encoding.ASCII.GetBytes(salt + password));

                return salt + Convert.ToBase64String(bytes) == hash;
            }
        }

        public string GenerateHash(string password)
        {
            var salt = GenerateSalt();

            using (var algorithm = SHA256.Create())
            {
                var bytes = algorithm.ComputeHash(Encoding.ASCII.GetBytes(salt + password));

                return salt + Convert.ToBase64String(bytes);
            }
        }

        private string GenerateSalt()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < options.SaltLength; i++)
            {
                var index = random.Next(options.SaltCharacters.Length);
                sb.Append(options.SaltCharacters[index]);
            }

            return sb.ToString();
        }
    }
}
