using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AccountContext _accountContext;
        private readonly IHashService _hashService;
        private readonly IIdentityService _identityService;

        [BindProperty]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RegisterModel(AccountContext accountContext, IHashService hashService, IIdentityService identityService)
        {
            _accountContext = accountContext;
            _hashService = hashService;
            _identityService = identityService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ValidateInput())
                return Page();

            if(_accountContext.Accounts.Any(a => string.Compare(a.Username, Username) == 0))
            {
                ModelState.AddModelError(nameof(Username), "Name already taken!");
                return Page();
            }

            var account = new Domain.ValueTypes.Account
            {
                Username = Username,
                Email = Email,
                IsEmailVerified = false,
                HashedPassword = _hashService.Hash(Password)
            };

            _accountContext.Accounts.Add(account);
            await _accountContext.SaveChangesAsync();

            await _identityService.LogIn(account);

            return RedirectToPage("/Account/Login");
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(Username))
            {
                ModelState.AddModelError(nameof(Username), string.Empty);
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(nameof(Password), string.Empty);
                return false;
            }

            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError(nameof(Email), string.Empty);
                return false;
            }

            return true;
        }
    }
}