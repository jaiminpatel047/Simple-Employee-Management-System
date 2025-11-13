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
    public class DepartmentsRepository : Repostiory<Department>, IDepartmentsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> filter = null)
        {
            try
            {
                IQueryable<Department> queryable = _dbContext.Departments.AsNoTracking();

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
        public async Task<Department> GetActiveByIdAsync(int id)
        {
            try
            {
                var detail = await _dbContext.Departments
                                   .AsNoTracking().Where(x => x.IsActive == true)
                                   .FirstOrDefaultAsync(x => x.Id == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Department>> GetAllActiveAsync()
        {
            return await _dbContext.Departments.AsNoTracking().Where(x => x.IsActive == true).ToListAsync();
        }
    }
}
