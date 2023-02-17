using Expensier.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.EntityFramework
{
    public class ExpensierDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<CryptoAsset> CryptoAssets { get; set; }

        public ExpensierDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoAsset>().OwnsOne(c => c.Crypto);
            modelBuilder.Entity<User>().Property(p => p.Profile_Picture).HasDefaultValue("/Styles/Images/default_profile_picture.png");
            base.OnModelCreating(modelBuilder);
        }
    }
}
