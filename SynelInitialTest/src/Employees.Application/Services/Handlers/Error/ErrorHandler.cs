using Employees.Core.Entities;
namespace Employees.Application.Services.Handlers.Error
{
    public class ErrorHandler : IErrorHandler
    {
        public void HandleAddError(Employee employee, Exception ex, List<string> errorMessages)
        {
            errorMessages.Add($"Error adding employee with PayrollNumber {employee.PayrollNumber}: {ex.Message}");
        }

        public void HandleSaveError(Employee employee, Exception ex, List<string> errorMessages)
        {
            errorMessages.Add($"Error saving employee with PayrollNumber {employee.PayrollNumber}: {ex.Message}");
        }
    }

}
