using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public interface IGeneric<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(string id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(string id);
        Task<T> FindByIdAsync(string id);
    }
}
