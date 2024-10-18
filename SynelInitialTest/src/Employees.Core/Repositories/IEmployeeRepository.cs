
using Employees.Core.Entities;

namespace Employees.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IReadOnlyList<Employee>> getAllAsync();
        Task AddAsync(Employee employee);
        Task<bool> existingPayrollNumber(string payrollNumber);
        Task<int> SaveChangesAsync();
        
      
    }
}
