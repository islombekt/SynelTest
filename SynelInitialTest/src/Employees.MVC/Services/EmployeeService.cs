using AutoMapper;
using Employees.MVC.Data;
using Employees.MVC.Models.DTOs;
using Employees.MVC.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Employees.MVC.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<CreatingEmployees> AddRangeAsync(List<EmployeesDTO> newRecords)
        {
            try 
            {
               
                int AddedRecords = 0;
                // mapp to employee
                List<Employee> employees = _mapper.Map<List<Employee>>(newRecords);

                // not inserting With addRange in order to catch any of errors
                foreach (Employee employee in employees)
                {
                    try 
                    { 
                        _context.Employees.Add(employee);
                    }
                    catch (Exception ex) 
                    { 
                       Console.WriteLine(ex.InnerException); 
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                
            }
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeesAsync()
        {
            List<Employee> emp = await _context.Employees
                                               .AsNoTracking()
                                               .ToListAsync();
            return emp;
        }
    }
}
