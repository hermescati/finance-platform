using Microsoft.EntityFrameworkCore;


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
