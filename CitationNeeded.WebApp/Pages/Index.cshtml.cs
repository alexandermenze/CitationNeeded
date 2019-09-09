using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CitationNeeded.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<string> BlaBla { get; set; }

        public void OnGet()
        {
            BlaBla = new List<string>()
            {
                "Eins",
                "Zwei",
                "Drie",
                "Vier"
            };
        }
    }
}