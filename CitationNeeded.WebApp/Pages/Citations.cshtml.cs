using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitationNeeded.WebApp.Pages
{
    [Authorize]
    public class CitationsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}