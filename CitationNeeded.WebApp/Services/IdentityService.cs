using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Account GetIdentity()
        {
            var account = new Account
            {
                Id = GetClaimValue(ClaimTypes.NameIdentifier),
                Username = GetClaimValue(ClaimTypes.Name),
                Email = GetClaimValue(ClaimTypes.Email)
            };

            if (account.Id == null || account.Username == null || account.Email == null)
            {
                throw new IdentityException(
                    $"Error reading identity values! Id: {account.Id}, Username: {account.Username}, Email: {account.Email}");
            }

            return account;
        }

        public async Task LogIn(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id),
                new Claim(ClaimTypes.Name, account.Username),
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

        private string GetClaimValue(string claimType)
        {
            return _httpContextAccessor
                .HttpContext
                ?.User
                ?.FindFirstValue(claimType);
        }

        private HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext 
                ?? throw new IdentityException("Identity service can only be used during request!");
        }
    }
}
