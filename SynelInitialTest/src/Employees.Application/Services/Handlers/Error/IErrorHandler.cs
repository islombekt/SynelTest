
using Employees.Core.Entities;

namespace Employees.Application.Services.Handlers.Error
{
    public interface IErrorHandler
    {
        void HandleAddError(Employee employee, Exception ex, List<string> errorMessages);
        void HandleSaveError(Employee employee, Exception ex, List<string> errorMessages);
    }
}
