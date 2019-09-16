using CitationNeeded.Domain.ValueTypes.Account;
using Microsoft.EntityFrameworkCore;

namespace CitationNeeded.Database.Database
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
    }
}
