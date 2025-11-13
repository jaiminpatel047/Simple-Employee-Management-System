using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IRepostiory<T> where T : class
    {
        Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includePropeties = null);
        Task<T> GetByIdAsync(int id);
        Task SaveAsync();
        void Save();
        Task<bool> IsExistByNameAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includePropeties = null);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
    }
}
