using Expensier.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;


namespace Expensier.EntityFramework.Services
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly ExpensierDbContextFactory _contextFactory;


        public NonQueryDataService( ExpensierDbContextFactory contextFactory )
        {
            _contextFactory = contextFactory;
        }


        public async Task<T> Create( T entity )
        {
            using ( ExpensierDbContext context = _contextFactory.CreateDbContext() )
            {
                EntityEntry<T> newEntity = await context.Set<T>().AddAsync( entity );
                await context.SaveChangesAsync();

                return newEntity.Entity;
            }
        }


        public async Task<T> Update( Guid id, T entity )
        {
            using ( ExpensierDbContext context = _contextFactory.CreateDbContext() )
            {
                entity.ID = id;

                context.Set<T>().Update( entity );
                await context.SaveChangesAsync();

                return entity;
            }
        }


        public async Task<bool> Delete( Guid id )
        {
            using ( ExpensierDbContext context = _contextFactory.CreateDbContext() )
            {
                T? entity = await context.Set<T>().SingleOrDefaultAsync( ( e ) => e.ID == id );

                if ( entity == null ) return false;

                context.Set<T>().Remove( entity );
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
