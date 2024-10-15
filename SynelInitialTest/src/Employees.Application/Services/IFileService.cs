using Employees.Application.Responses;
using Microsoft.AspNetCore.Http;

namespace Employees.Application.Services
{
    public interface IFileService
    {
        Task<Response<CreatingEmployees>> addRecords(IFormFile file);
    }
}
