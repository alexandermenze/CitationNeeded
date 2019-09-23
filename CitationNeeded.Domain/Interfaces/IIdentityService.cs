using CitationNeeded.Domain.ValueTypes;
using System.Threading.Tasks;

namespace CitationNeeded.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task LogIn(Account account);
        Task LogOut();
        Task<bool> CheckEmailVerified(string email);
        Account GetIdentity();
    }
}
