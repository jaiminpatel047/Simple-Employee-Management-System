using Microsoft.EntityFrameworkCore;
using SimpleCurd.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Repository
{
    public class Repostiory<T> : IRepostiory<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;
      
        public Repostiory(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task AddAsync(T Entity)
        {
            try
            {
               await _dbset.AddAsync(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T Entity)
        {
            try
            {
                _dbset.Update(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T Entity)
        {
            try
            {
                _dbset.Update(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includePropeties = null)
        {
            try
            {
                IQueryable<T> query = _dbset.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(includePropeties))
                {
                    foreach (var includeProp in includePropeties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }

                if (filter != null)
                {
                    return await query.Where(filter).ToListAsync();
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbset.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includePropeties = null)
        {
            try
            {
                IQueryable<T> entity =  _dbset.AsNoTracking();
                entity = entity.Where(filter);

                if (!string.IsNullOrWhiteSpace(includePropeties))
                {
                    foreach (var includeProp in includePropeties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        entity = entity.Include(includeProp);
                    }
                }

                return await entity.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<bool> IsExistByNameAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.AnyAsync(predicate);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return await _dbset.CountAsync(filter);
                }
                return await _dbset.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
