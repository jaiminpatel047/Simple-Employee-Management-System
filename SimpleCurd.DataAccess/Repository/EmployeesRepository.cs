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
    public class EmployeesRepository : Repostiory<Employee>, IEmployeesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Employee>> GetAllActiveAsync(Expression<Func<Employee, bool>> filter = null)
        {
            try
            {
                IQueryable<Employee> queryable = _dbContext.Employees.AsNoTracking().Where(x => x.IsActive == false);

                if(filter != null)
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
        public async Task<Employee> GetActiveByIdAsync(int id)
        {
            try
            {
                var detail = await _dbContext.Employees
                                   .AsNoTracking().Where(x => x.IsActive == false)
                                   .FirstOrDefaultAsync(x => x.Id == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EmployeeExistsAsync(int id)
        {
            return _dbContext.Employees
                .AsNoTracking()
                .Any(e => e.Id == id && !e.IsActive);
        }
    }
}
