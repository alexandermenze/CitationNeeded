using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public Domain.ValueTypes.Account Account { get; set; }

        public IndexModel(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Account = _identityService.GetIdentity();
                await _emailService.SendAsync(new Email
                {
                    From = "test@alexandermenze.de",
                    To = Account.Email,
                    Subject = "alexandermenze.de: Email verification code",
                    Content = "Your code is 12345!"
                });
            }
            catch (IdentityException)
            {
            }
        }
    }
}