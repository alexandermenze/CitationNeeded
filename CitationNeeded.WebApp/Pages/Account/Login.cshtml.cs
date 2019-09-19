using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ICredentialVerifier _credentialVerifier;
        private readonly IIdentityService _identityService;
        private readonly AccountContext _accountContext;

        [BindProperty]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(AccountContext accountContext, ICredentialVerifier credentialVerifier, IIdentityService identityService)
        {
            _credentialVerifier = credentialVerifier;
            _identityService = identityService;
            _accountContext = accountContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var credentials = new Credentials { Username = Username, Password = Password };

            if(!await _credentialVerifier.VerifyAsync(credentials))
            {
                ModelState.AddModelError(nameof(Username), "Invalid credentials");
                ModelState.AddModelError(nameof(Password), "Invalid credentials");
                return Page();
            }

            await _identityService.LogIn(GetAccount(credentials.Username));

            return RedirectToPage("/Index");
        }

        private Domain.ValueTypes.Account GetAccount(string username)
        {
            return _accountContext.Accounts.Single(a => string.CompareOrdinal(a.Username, username) == 0);
        }
    }
}