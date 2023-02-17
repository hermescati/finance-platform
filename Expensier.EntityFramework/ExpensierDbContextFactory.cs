using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.EntityFramework
{
    public class ExpensierDbContextFactory : IDesignTimeDbContextFactory<ExpensierDbContext>
    {
        public ExpensierDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ExpensierDbContext>();
            options.UseSqlServer("Server=localhost\\sqlexpress;Database=expensier_test;Trusted_Connection=True;Encrypt=False;");
            
            return new ExpensierDbContext(options.Options);
        }
    }
}
