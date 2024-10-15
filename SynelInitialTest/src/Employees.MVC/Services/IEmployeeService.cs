using Employees.MVC.Models.DTOs;
using Employees.MVC.Models.Entities;

namespace Employees.MVC.Services
{
    public interface IEmployeeService
    {
        Task<CreatingEmployees> AddRangeAsync(List<EmployeesDTO> newRecords);
        Task<IReadOnlyList<Employee>> GetEmployeesAsync();
    }
}
