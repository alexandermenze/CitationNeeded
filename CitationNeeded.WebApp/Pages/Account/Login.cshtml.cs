﻿using CitationNeeded.Database.Database;
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
        private readonly CitationContext _citationContext;

        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(CitationContext citationContext, ICredentialVerifier credentialVerifier, IIdentityService identityService)
        {
            _credentialVerifier = credentialVerifier;
            _identityService = identityService;
            _citationContext = citationContext;
        }

        public IActionResult OnGet()
        {
            if (_identityService.IsLoggedIn())
                return Redirect("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var credentials = new Credentials { Email = Email, Password = Password };

            if(!await _credentialVerifier.VerifyAsync(credentials))
            {
                ModelState.AddModelError(nameof(Email), string.Empty);
                ModelState.AddModelError(nameof(Password), string.Empty);
                return Page();
            }

            if(!await _identityService.CheckEmailVerified(Email))
            {
                ModelState.AddModelError(nameof(Email), string.Empty);
                return Page();
            }

            await _identityService.LogIn(GetAccount(credentials.Email));

            return RedirectToPage("/Index");
        }

        private Domain.ValueTypes.Account GetAccount(string email)
        {
            return _citationContext.Accounts.Single(a => a.Email == email);
        }
    }
}