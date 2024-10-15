using CsvHelper;
using Employees.MVC.Models.DTOs;
using Employees.MVC.Models.Entities;
using System.Formats.Asn1;
using System.Globalization;

namespace Employees.MVC.Services
{
    public class FileService : IFileService
    {
        private readonly IEmployeeService _employeeService;
        public FileService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<Response<CreatingEmployees>> addRecords(IFormFile file)
        {
            // initialize response
            var response = new Response<CreatingEmployees>();

            // check if the file exists and has data
            if (file == null || file.Length == 0)
            {
                response.Message = "File is empty.";
                response.IsSuccess = false;
                return response;
            }
            
            // read from file 
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var employees = new List<Employee>();

                    // Assuming using CsvHelper for parsing CSV
                    var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    var records = csv.GetRecords<EmployeesDTO>().ToList();

                    // Add records to the database
                   var added = await _employeeService.AddRangeAsync(records);

                    response.Data = added;
                    response.IsSuccess = true;
                    response.Message = $"{records.Count} employees added successfully.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
           
        }

       
    }
}
