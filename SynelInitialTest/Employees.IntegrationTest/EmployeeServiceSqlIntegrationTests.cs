using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Xunit;
using Employees.MVC.Services;
using Employees.MVC.Data;
using AutoMapper;
using Moq;
using AutoMapper;
using Employees.MVC.Models.Entities;
using Employees.MVC.Models.DTOs;

namespace Employees.IntegrationTest
{
    public class EmployeeServiceSqlIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IMapper> _mapper;
        public EmployeeServiceSqlIntegrationTests()
        {
            // Set up a real MS SQL LocalDB (or SQL Server) connection
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Data Source=ICT-ITOXOROV1;Database=SynelTest_IntegrationTest;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            // Apply migrations or ensure database is created
            _context.Database.Migrate();

            _mapper = new Mock<IMapper>();
            // Setup a mock for the mapping logic (assume EmployeesDTO to Employee mapping)
            _mapper.Setup(m => m.Map<List<Employee>>(It.IsAny<List<EmployeesDTO>>()))
                .Returns(new List<Employee> { new Employee { PayrollNumber = "COOP08", Forenames = "John", Surname = "Doe" } });
          

            _employeeService = new EmployeeService(_context, _mapper.Object);
        }

        [Fact]
        public async Task AddRecords_ShouldInsertEmployees_WhenCsvIsValid()
        {
            // Arrange
            var csvData = @"PayrollNumber,Forenames,Surname,DateOfBirth,Telephone,Mobile,Address,Address2,Postcode,EmailHome,StartDate
                        COOP08,John,William,26/01/1955,12345678,987654321,12 Foreman road,,GU12 6JW,nomadic20@hotmail.co.uk,18/04/2013";

            // Create a mock file using IFormFile (simulating a file upload)
            var file = CreateMockFormFile(csvData, "employees.csv");

            // Act
            var result = await _employeeService.AddRangeAsync(new List<EmployeesDTO>());

            // Assert
          //  Assert.True(result.IsSuccess);
          //  Assert.Equal(1, result.Data.RecordsProcessed);  // Only one employee in CSV

            var insertedEmployee = _context.Employees.FirstOrDefault(e => e.PayrollNumber == "COOP08");
            Assert.NotNull(insertedEmployee);
            Assert.Equal("John", insertedEmployee.Forenames);
            Assert.Equal("William", insertedEmployee.Surname);
        }

        // Helper method to create a mock IFormFile from string data
        private IFormFile CreateMockFormFile(string content, string fileName)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/csv"
            };
        }

        // Cleanup the database after each test
        public void Dispose()
        {
            _context.Database.EnsureDeleted();  // Cleanup the database after test
            _context.Dispose();
        }
    }
}
