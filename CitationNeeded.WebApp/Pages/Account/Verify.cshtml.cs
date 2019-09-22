using CitationNeeded.Database.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class VerifyModel : PageModel
    {
        private readonly AccountContext _accountContext;

        public VerifyModel(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] string token)
        {
            if (token == null)
                return Redirect("/Account/Login");

            var verification = await _accountContext
                .AccountVerifications
                .SingleOrDefaultAsync(a => string.CompareOrdinal(a.VerificationToken, token) == 0);

            if (verification == null)
                return Redirect("/Account/Login");

            verification.IsVerified = true;

            await _accountContext.SaveChangesAsync();

            return Redirect("/Account/Login");
        }
    }
}