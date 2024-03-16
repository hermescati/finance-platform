using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Microsoft.EntityFrameworkCore;


namespace Expensier.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly ExpensierDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;


        public GenericDataService( ExpensierDbContextFactory contextFactory )
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>( contextFactory );
        }


        public async Task<T> Create( T entity )
        {
            return await _nonQueryDataService.Create( entity );
        }


        public async Task<T> Update( Guid id, T entity )
        {
            return await _nonQueryDataService.Update( id, entity );
        }


        public async Task<bool> Delete( Guid id )
        {
            return await _nonQueryDataService.Delete( id );
        }


        public async Task<T> GetByID( Guid id )
        {
            using ( ExpensierDbContext context = _contextFactory.CreateDbContext() )
            {
                T? entity = await context.Set<T>().SingleOrDefaultAsync( ( e ) => e.ID == id );

                return entity;
            }
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            using ( ExpensierDbContext context = _contextFactory.CreateDbContext() )
            {
                return await context.Set<T>().ToListAsync();
            }
        }
    }
}
