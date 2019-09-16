using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ICredentialVerifier _credentialVerifier;

        [BindProperty]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(ICredentialVerifier credentialVerifier)
        {
            _credentialVerifier = credentialVerifier;
        }

        public async Task OnPostAsync()
        {
            var credentials = new Credentials { Username = Username, Password = Password };

            if(!await _credentialVerifier.VerifyAsync(credentials))
            {
                ModelState.AddModelError(nameof(Username), "Invalid credentials");
                ModelState.AddModelError(nameof(Password), "Invalid credentials");
                return;
            }

            
        }
    }
}