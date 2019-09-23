using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Services
{
    public class IdentityService : IIdentityService
    {
        private const string FirstNameClaimType = "FirstName";
        private const string LastNameClaimType = "LastName";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AccountContext _accountContext;

        public IdentityService(IHttpContextAccessor httpContextAccessor, AccountContext accountContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountContext = accountContext;
        }

        public Account GetIdentity()
        {
            var account = new Account
            {
                Id = GetClaimValue(ClaimTypes.NameIdentifier),
                FirstName = GetClaimValue(FirstNameClaimType),
                LastName = GetClaimValue(LastNameClaimType),
                Email = GetClaimValue(ClaimTypes.Email)
            };

            return account;
        }

        public async Task LogIn(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id),
                new Claim(FirstNameClaimType, account.FirstName),
                new Claim(LastNameClaimType, account.LastName),
                new Claim(ClaimTypes.Email, account.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await GetHttpContext().SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public async Task LogOut()
        {
            await GetHttpContext().SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> CheckEmailVerified(string email)
        {
            var verifications = await _accountContext
                .AccountVerifications
                .Where(a => string.CompareOrdinal(a.Account.Email, email) == 0)
                .ToListAsync();

            return verifications.All(v => v.IsVerified);
        }

        private string GetClaimValue(string claimType)
        {
            return _httpContextAccessor
                .HttpContext
                ?.User
                ?.FindFirstValue(claimType)
                ?? throw new IdentityException($"Could not read identity claim value for {claimType}!");
        }

        private HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext 
                ?? throw new IdentityException("Identity service can only be used during request!");
        }
    }
}
