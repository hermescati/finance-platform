using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Doman.Services
{
    public interface IDataService<T>
    {
        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);

        Task<T> GetByID(int id);

        Task<IEnumerable<T>> GetAll();
    }
}
