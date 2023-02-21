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
    public class ExpensierDbContextFactory
    {
        private readonly string _connectionString;

        public ExpensierDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ExpensierDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ExpensierDbContext>();
            options.UseSqlServer(_connectionString);
            
            return new ExpensierDbContext(options.Options);
        }
    }
}
