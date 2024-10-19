using Employees.Core.Entities;
using Employees.Core.Repositories;
using Employees.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Employees.Infrastructure.Repositories
{
    // this service is used to follow the separation of concerns 
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
           await _context.Employees.AddAsync(employee);  
        }

        public async Task<bool> existingPayrollNumber(string payrollNumber)
        {
            return await _context.Employees
              .Where(e => e.PayrollNumber.Equals(payrollNumber))
              .AnyAsync();
            
        }

        public async Task<IReadOnlyList<Employee>> getAllAsync()
        {
            // default order by Employee SurName
            return await _context.Employees
                            .OrderBy(e => e.Surname)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); 
        }
    }

}
