using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class VerificationModel : PageModel
    {
        public IActionResult OnPost()
        {
            return RedirectToPage("/Account/Login");
        }
    }
}