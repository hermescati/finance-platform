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
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public ExpensierDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public ExpensierDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ExpensierDbContext>();
            
            _configureDbContext(options);
            
            return new ExpensierDbContext(options.Options);
        }
    }
}
