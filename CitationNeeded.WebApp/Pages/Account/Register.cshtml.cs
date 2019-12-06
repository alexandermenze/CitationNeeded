using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly RNGCryptoServiceProvider _cryptoService = new RNGCryptoServiceProvider();
        private readonly CitationContext _citationContext;
        private readonly IHashService _hashService;
        private readonly IEmailService _emailService;

        [BindProperty]
        [Required]
        public string FirstName { get; set; }
        [BindProperty]
        [Required]
        public string LastName { get; set; }
        [BindProperty]
        [StringLength(maximumLength: 100, MinimumLength = 6)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RegisterModel(CitationContext citationContext, IHashService hashService, IEmailService emailService)
        {
            _citationContext = citationContext;
            _hashService = hashService;
            _emailService = emailService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ValidateInput())
                return Page();

            var account = await RegisterAccountAsync();
            await SendRegisterEmailAsync(account);

            return RedirectToPage("/Account/Login");
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                ModelState.AddModelError(nameof(FirstName), string.Empty);
                return false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                ModelState.AddModelError(nameof(LastName), string.Empty);
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

            if (_citationContext.Accounts.Any(a => a.Email.ToUpperInvariant() == Email.ToUpperInvariant()))
            {
                ModelState.AddModelError(nameof(Email), "Email already taken!");
                return false;
            }

            return true;
        }

        private async Task<Domain.ValueTypes.Account> RegisterAccountAsync()
        {
            var account = new Domain.ValueTypes.Account
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                HashedPassword = _hashService.Hash(Password)
            };

            _citationContext.Accounts.Add(account);
            await _citationContext.SaveChangesAsync();

            return account;
        }

        private async Task SendRegisterEmailAsync(Domain.ValueTypes.Account account)
        {
            var requestUrl = $"https://{Request.Host}";

            var token = GenerateToken();
            var tokenUrl = QueryHelpers.AddQueryString($"{requestUrl}/Account/Verify", "token", token);

            _citationContext.AccountVerifications.Add(new AccountVerification
            {
                Account = account,
                IsVerified = false,
                VerificationToken = token
            });

            await _emailService.SendAsync(new Email
            {
                From = "account@alexandermenze.de",
                To = account.Email,
                Subject = "Email verification for CitationNeeded",
                Content = $"Your verfication link: \n<a href=\"{tokenUrl}\">{tokenUrl}</a>"
            });
            
            await _citationContext.SaveChangesAsync();
        }

        private string GenerateToken()
        {
            var bytes = new byte[32];
            _cryptoService.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}