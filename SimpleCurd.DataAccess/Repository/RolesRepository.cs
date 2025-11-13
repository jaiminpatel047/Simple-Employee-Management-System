using Microsoft.EntityFrameworkCore;
using SimpleCurd.DataAccess.Interface;
using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Repository
{
    public class RolesRepository : Repostiory<Role>, IRolesRepository
    {
        private readonly ApplicationDbContext _dbContext;   
        public RolesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _dbContext.Roles.AsNoTracking().Where(x => x.IsActive == true).ToListAsync();
        }
        public async Task<IEnumerable<Role>> GetAllAsync(Expression<Func<Role, bool>> filter = null)
        {
            try
            {
                IQueryable<Role> queryable = _dbContext.Roles.AsNoTracking();

                if (filter != null)
                {
                    queryable = queryable.Where(filter);
                }
                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Role> GetActiveByIdAsync(int id)
        {
            try
            {
                var detail = await _dbContext.Roles
                                   .AsNoTracking().Where(x => x.IsActive == true)
                                   .FirstOrDefaultAsync(x => x.Id == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Role>> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var roles = await _dbContext.Roles.Where(x => x.Name == name).ToListAsync();

            return roles;
        }

        
    }
}
