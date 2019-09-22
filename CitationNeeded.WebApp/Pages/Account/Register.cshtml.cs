using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        private readonly AccountContext _accountContext;
        private readonly IHashService _hashService;
        private readonly IEmailService _emailService;

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

        public RegisterModel(AccountContext accountContext, IHashService hashService, IEmailService emailService)
        {
            _accountContext = accountContext;
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

            if (_accountContext.Accounts.Any(a => string.Compare(a.Username, Username) == 0))
            {
                ModelState.AddModelError(nameof(Username), "Name already taken!");
                return false;
            }

            if (_accountContext.Accounts.Any(a => string.Compare(a.Email, Email) == 0))
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
                Username = Username,
                Email = Email,
                HashedPassword = _hashService.Hash(Password)
            };

            _accountContext.Accounts.Add(account);
            await _accountContext.SaveChangesAsync();

            return account;
        }

        private async Task SendRegisterEmailAsync(Domain.ValueTypes.Account account)
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host}";

            var token = GenerateToken();

            _accountContext.AccountVerifications.Add(new AccountVerification
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
                Content = $"Your verfication link: {requestUrl}/Account/Verify?token={token}"
            });
            
            await _accountContext.SaveChangesAsync();
        }

        private string GenerateToken()
        {
            var bytes = new byte[32];
            _cryptoService.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}