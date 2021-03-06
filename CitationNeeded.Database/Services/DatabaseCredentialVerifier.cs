﻿using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CitationNeeded.Database.Services
{
    public class DatabaseCredentialVerifier : ICredentialVerifier
    {
        private readonly CitationContext _accountContext;
        private readonly IHashService _hashService;

        public DatabaseCredentialVerifier(CitationContext dbContext, IHashService secureHashService)
        {
            _accountContext = dbContext;
            _hashService = secureHashService;
        }

        public async Task<bool> VerifyAsync(Credentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Password))
                return false;

            var user = await _accountContext.Accounts.SingleOrDefaultAsync(u => u.Email.Equals(credentials.Email))
                .ConfigureAwait(false);

            return user == null
                ? false
                : _hashService.Verify(credentials.Password, user.HashedPassword);
        }
    }
}
