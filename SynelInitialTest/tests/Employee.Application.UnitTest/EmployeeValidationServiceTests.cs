using Employees.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Application.UnitTest
{
    public class EmployeeValidationServiceTests
    {
        private readonly EmployeeValidationService _validationService;

        public EmployeeValidationServiceTests()
        {
            // Arrange: Instantiate the service to be tested
            _validationService = new EmployeeValidationService();
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenPayrollNumberIsEmpty()
        {
            // Arrange
            var employee = new Employees.Core.Entities.Employee
            {
                PayrollNumber = "",
                Forenames = "John",
                Surname = "Doe",
                DateOfBirth = DateTime.Now.AddYears(-30),
                Telephone = "123456789",
                Mobile = "987654321",
                Address = "123 Main St",
                EmailHome = "john.doe@example.com",
                StartDate = DateTime.Now
            };

            // Act
            var isValid = _validationService.Validate(employee, out var validationError);

            // Assert
            Assert.False(isValid);
            Assert.Equal("PayrollNumber cannot be empty.", validationError);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenPayrollNumberIsNotEmpty()
        {
            // Arrange
            var employee = new Employees.Core.Entities.Employee
            {
                PayrollNumber = "EMP001",
                Forenames = "Jane",
                Surname = "Smith",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Telephone = "987654321",
                Mobile = "123456789",
                Address = "456 Another St",
                EmailHome = "jane.smith@example.com",
                StartDate = DateTime.Now
            };

            // Act
            var isValid = _validationService.Validate(employee, out var validationError);

            // Assert
            Assert.True(isValid);
            Assert.Null(validationError);
        }
    }
}
