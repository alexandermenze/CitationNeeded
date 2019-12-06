using CitationNeeded.Database.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class VerifyModel : PageModel
    {
        private readonly CitationContext _citationContext;

        public VerifyModel(CitationContext citationContext)
        {
            _citationContext = citationContext;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] string token)
        {
            if (token == null)
                return Redirect("/Account/Login");

            var verification = await _citationContext
                .AccountVerifications
                .SingleOrDefaultAsync(a => string.CompareOrdinal(a.VerificationToken, token) == 0);

            if (verification == null)
                return Redirect("/Account/Login");

            verification.IsVerified = true;

            await _citationContext.SaveChangesAsync();

            return Redirect("/Account/Login");
        }
    }
}