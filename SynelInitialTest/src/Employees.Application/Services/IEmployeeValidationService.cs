
using Employees.Core.Entities;

namespace Employees.Application.Services
{
    public interface IEmployeeValidationService
    {
        bool Validate(Employee employee, out string validationError);
    }
}
