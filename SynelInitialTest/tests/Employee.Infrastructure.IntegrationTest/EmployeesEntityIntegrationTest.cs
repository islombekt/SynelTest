using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.IntegrationTest
{
    public class EmployeesEntityIntegrationTest : BaseIntegrationTest
    {
        public EmployeesEntityIntegrationTest(IntegrationTestDbContextFactory factory)
      : base(factory) { }

        [Fact]
        public async Task AddEntity_Should_Insert_Record_In_Database()
        {
            // Arrange
            var entity = new Employees.Core.Entities.Employee
            {
                PayrollNumber = "EMP002",
                Forenames = "Someone",
                Surname = "Surname",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Telephone = "987654321",
                Mobile = "123456789",
                Address = "257 sttr",
                Address2 = "",
                Postcode = "",
                EmailHome = "someone@example.com",
                StartDate = DateTime.Now,
            };

            // Act
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            // Assert
            var insertedEntity = await _dbContext.Employees.FindAsync(entity.EmployeeId);
            Assert.NotNull(insertedEntity);
            Assert.Equal("EMP002", insertedEntity.PayrollNumber);
        }

        [Fact]
        public async Task GetEntity_Should_Return_Correct_Data()
        {
            // Act
            var entity = await _dbContext.Employees.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(entity);
            Assert.Equal("EMP002", entity.PayrollNumber);  // Assuming it was seeded
        }
    }
}
