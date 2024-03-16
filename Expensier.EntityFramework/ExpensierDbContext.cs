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
        public DbSet<CryptoAsset> CryptoAssets { get; set; }
        public DbSet<PortfolioReturn> PortfolioReturns { get; set; }


        public ExpensierDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoAsset>().OwnsOne(c => c.Asset);
            base.OnModelCreating(modelBuilder);
        }
    }
}
