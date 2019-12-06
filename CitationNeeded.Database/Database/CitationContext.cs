using CitationNeeded.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace CitationNeeded.Database.Database
{
    public class CitationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountVerification> AccountVerifications { get; set; }
        public DbSet<CitationBook> CitationBooks { get; set; }

        public CitationContext(DbContextOptions<CitationContext> options)
            : base(options)
        {
        }
    }
}
