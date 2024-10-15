using Employees.MVC.Models.DTOs;

namespace Employees.MVC.Services
{
    public interface IFileService
    {
        Task<Response<CreatingEmployees>> addRecords(IFormFile file);
    }
}
