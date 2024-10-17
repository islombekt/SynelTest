using Employees.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.IntegrationTest
{
    // To clean database in each test, and rollback changes
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestDbContextFactory>
    {
        protected readonly ApplicationDbContext _context;
        private readonly IntegrationTestDbContextFactory _factory;

        public BaseIntegrationTest(IntegrationTestDbContextFactory factory)
        {
            _factory = factory;
            var scope = _factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
    }

    public class EmployeeIntegrationTest : BaseIntegrationTest
    {
        public EmployeeIntegrationTest(IntegrationTestDbContextFactory factory)
            : base(factory) { }

        [Fact]
        public async Task Can_Insert_Employee()
        {
            // Arrange
            var employee = new Employees.Core.Entities.Employee
            {
                PayrollNumber = "EMP003",
                Forenames = "John",
                Surname = "Doe",
                DateOfBirth = DateTime.Now.AddYears(-30),
                Telephone = "123456789",
                Mobile = "987654321",
                Address = "123 Test St",
                EmailHome = "john.doe@example.com",
                StartDate = DateTime.Now
            };

            // Act
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Assert
            var insertedEmployee = await _context.Employees.FindAsync(employee.EmployeeId);
            Assert.NotNull(insertedEmployee);
            Assert.Equal("EMP003", insertedEmployee.PayrollNumber);
        }
    }



}
