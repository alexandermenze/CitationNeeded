using CitationNeeded.Domain.ValueTypes.Account;
using System.Threading.Tasks;

namespace CitationNeeded.Domain.Interfaces
{
    public interface ICredentialVerifier
    {
        Task<bool> VerifyAsync(Credentials credentials);
    }
}
