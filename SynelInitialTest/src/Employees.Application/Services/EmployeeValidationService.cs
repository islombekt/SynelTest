
using Employees.Core.Entities;

namespace Employees.Application.Services
{
    public class EmployeeValidationService : IEmployeeValidationService
    {
        public bool Validate(Employee employee, out string validationError)
        {
            validationError = null;

            // Example validation logic
            if (string.IsNullOrWhiteSpace(employee.PayrollNumber))
            {
                validationError = "PayrollNumber cannot be empty.";
                return false;
            }
                
            return true;
        }
    }

}
