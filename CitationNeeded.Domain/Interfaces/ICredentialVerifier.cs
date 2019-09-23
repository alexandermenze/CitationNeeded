using CitationNeeded.Domain.ValueTypes;
using System.Threading.Tasks;

namespace CitationNeeded.Domain.Interfaces
{
    public interface ICredentialVerifier
    {
        Task<bool> VerifyAsync(Credentials credentials);
    }
}
