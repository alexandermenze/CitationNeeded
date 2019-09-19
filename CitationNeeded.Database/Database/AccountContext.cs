using CitationNeeded.Domain.ValueTypes;
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
