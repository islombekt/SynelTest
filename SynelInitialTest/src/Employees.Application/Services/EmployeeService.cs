using AutoMapper;
using Employees.Application.Responses;
using Employees.Application.Services.Handlers.Error;
using Employees.Core.Entities;
using Employees.Core.Repositories;
namespace Employees.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository; 
        private readonly IEmployeeValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly IErrorHandler _errorHandler;
        public EmployeeService(
       IEmployeeRepository employeeRepository,
       IEmployeeValidationService validationService,
       IMapper mapper,
       IErrorHandler errorHandler)
        {
            _employeeRepository = employeeRepository;
            _validationService = validationService;
            _mapper = mapper;
            _errorHandler = errorHandler;
        }
        // to add records from csv file to database by validating and catching all errors
        public async Task<CreatingEmployees> AddRangeAsync(List<EmployeesDTO> newRecords)
        {
            var response = new CreatingEmployees();
            int successfullyAddedRecords = 0;
            int failedRecords = 0;
            var errorMessages = new List<string>();
            List<Employee> employees = new List<Employee>();
            try
            {
                // Map DTOs to Employee entities
                employees = _mapper.Map<List<Employee>>(newRecords);

                // Validate and add records
                foreach (Employee employee in employees)
                {
                    try
                    {
                        // validate that payroll number is not empty
                        if (!_validationService.Validate(employee, out var validationError))
                        {
                            failedRecords++;
                            errorMessages.Add($"Validation error for PayrollNumber {employee.PayrollNumber}: {validationError}");
                            continue;
                        }
                        if(await _employeeRepository.existingPayrollNumber(employee.PayrollNumber))
                        {
                            failedRecords++;
                            errorMessages.Add($"Existing PayrollNumber {employee.PayrollNumber}");
                            continue;
                        }
                        // call repository to add record
                        await _employeeRepository.AddAsync(employee);
                        successfullyAddedRecords++;
                    }
                    catch (Exception ex)
                    {
                        failedRecords++;
                        _errorHandler.HandleAddError(employee, ex, errorMessages);
                    }
                }

                // Try to save all the added records in bulk

                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // If SaveChanges fails, handle errors for specific records
                foreach (var employee in employees)
                {
                    _errorHandler.HandleSaveError(employee, ex, errorMessages);
                }

                failedRecords = employees.Count;
                successfullyAddedRecords = 0; // Reset, as nothing was saved
            }

            // Prepare the response
            response.TotalItems = employees.Count;
            response.Q_Added = successfullyAddedRecords;
            response.Q_Failure = failedRecords;
            response.Errors = errorMessages;

            return response;
        }

        // to get all Employees in database
        public async Task<IReadOnlyList<Employee>> GetEmployeesAsync()
        {
            return await _employeeRepository.getAllAsync();
        }
    }
}
