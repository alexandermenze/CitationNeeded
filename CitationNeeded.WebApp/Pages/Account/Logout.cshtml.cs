using CitationNeeded.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IIdentityService _identityService;

        public LogoutModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _identityService.LogOut();
            return RedirectToPage("/Account/Login");
        }
    }
}