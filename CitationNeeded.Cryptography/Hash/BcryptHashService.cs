using CitationNeeded.Domain.Interfaces;

namespace CitationNeeded.Cryptography.Hash
{
    public class BcryptHashService : IHashService
    {
        public string Hash(string input)
            => BCrypt.Net.BCrypt.HashPassword(input);

        public bool Verify(string input, string hashed)
            => BCrypt.Net.BCrypt.Verify(input, hashed);
    }
}
