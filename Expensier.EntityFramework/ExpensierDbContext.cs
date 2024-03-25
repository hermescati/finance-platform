using Expensier.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Expensier.EntityFramework
{
    public class ExpensierDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<AssetTransaction> AssetTransactions { get; set; }
        public DbSet<PortfolioReturn> PortfolioReturns { get; set; }


        public ExpensierDbContext( DbContextOptions options ) : base( options ) { }


        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<AssetTransaction>()
                .OwnsOne( c => c.Asset );

            modelBuilder.Entity<Subscription>()
                .Property( s => s.Status )
                .HasConversion<string>();

            modelBuilder.Entity<Subscription>()
                .Property( s => s.Frequency )
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property( t => t.Category )
                .HasConversion<string>();

            modelBuilder.Entity<AssetTransaction>()
                .Property( a => a.Category )
                .HasConversion<string>();

            base.OnModelCreating( modelBuilder );
        }
    }
}
