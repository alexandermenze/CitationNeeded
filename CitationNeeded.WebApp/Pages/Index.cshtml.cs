using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitationNeeded.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIdentityService _identityService;

        public Domain.ValueTypes.Account Account { get; set; }

        public IndexModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public void OnGet()
        {
            try
            {
                Account = _identityService.GetIdentity();
            }
            catch (IdentityException)
            {
            }
        }
    }
}