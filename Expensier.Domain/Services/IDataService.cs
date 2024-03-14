using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services
{
    public interface IDataService<T>
    {
        Task<T> Create( T entity );

        Task<T> Update( Guid id, T entity );

        Task<bool> Delete( Guid id );

        Task<T> GetByID( Guid id );

        Task<IEnumerable<T>> GetAll();
    }
}
