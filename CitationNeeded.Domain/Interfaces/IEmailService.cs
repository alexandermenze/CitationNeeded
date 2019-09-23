using CitationNeeded.Domain.ValueTypes;
using System.Threading.Tasks;

namespace CitationNeeded.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(Email email);
    }
}
