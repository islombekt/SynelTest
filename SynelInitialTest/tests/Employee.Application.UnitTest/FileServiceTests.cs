using Employees.Application.Responses;
using Employees.Application.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Application.UnitTest
{
    public class FileServiceTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly IFileService _fileService;

        public FileServiceTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _fileService = new FileService(_mockEmployeeService.Object);
        }

        [Fact]
        public async Task AddRecords_Should_Read_And_Convert_CSV_To_EmployeesDTO()
        {
            // Arrange
            string csvContent = @"Payroll_Number,Forenames,Surname,Date_of_Birth,Telephone,Mobile,Address,Address_2,Postcode,EMail_Home,StartDate
                                EMP001,Isl,Tokhirov,05/08/1997,123456789,987654321,123 Main St,add2,2312332,isl.tkh@example.com,05/08/2024
                                EMP002,Someone,Surname,05/08/2000,987654321,123456789,456 Oak Ave,add2,2312332,someone@example.com,05/08/2024";

            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvContent));
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(_ => _.OpenReadStream()).Returns(stream);
            fileMock.Setup(_ => _.Length).Returns(stream.Length);
            fileMock.Setup(_ => _.FileName).Returns("employees.csv");
            // Setup mock behavior for EmployeeService to return a CreatingEmployees instance
            var creatingEmployeesResponse = new CreatingEmployees
            {
                TotalItems = 2, 
                Q_Added = 2,
                Q_Failure = 0,
                Employees = new List<EmployeesDTO>
            {
                new EmployeesDTO { PayrollNumber = "EMP001", Forenames = "Isl" },
                new EmployeesDTO { PayrollNumber = "EMP002", Forenames = "Someone" }
            },
                Errors = new List<string>()
            };
            _mockEmployeeService.Setup(service => service.AddRangeAsync(It.IsAny<List<EmployeesDTO>>()))
                                .ReturnsAsync(creatingEmployeesResponse);
            // Act
            var result = await _fileService.addRecords(fileMock.Object);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Employees.Count); // Check if two employees were parsed from CSV
            // Additional assertions based on expected DTO content
            Assert.Equal("EMP001", result.Data.Employees[0].PayrollNumber);
            Assert.Equal("Isl", result.Data.Employees[0].Forenames);
            Assert.Equal("EMP002", result.Data.Employees[1].PayrollNumber);
            Assert.Equal("Someone", result.Data.Employees[1].Forenames);

            // Verify if EmployeeService method was called with correct DTOs
            _mockEmployeeService.Verify(service => service.AddRangeAsync(It.IsAny<List<EmployeesDTO>>()), Times.Once);
        }
    }
}
