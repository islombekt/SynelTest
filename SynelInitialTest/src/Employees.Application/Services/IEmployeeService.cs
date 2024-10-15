using Employees.Application.Responses;
using Employees.Core.Entities;

namespace Employees.Application.Services
{
    public interface IEmployeeService
    {
        Task<CreatingEmployees> AddRangeAsync(List<EmployeesDTO> newRecords);
        Task<IReadOnlyList<Employee>> GetEmployeesAsync();
    }
}
