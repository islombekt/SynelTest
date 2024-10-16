using AutoMapper;
using Employees.Application.Mappers;
using Employees.Application.Responses;
using Employees.Application.Services;
using Employees.Core.Repositories;
using Employees.Core.Entities;
using Moq;


namespace Employee.Application.UnitTest
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly Mock<IEmployeeValidationService> _mockValidationService;
        private readonly IMapper _mapper;

        public EmployeeServiceTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _mockValidationService = new Mock<IEmployeeValidationService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmployeeMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task AddRangeAsync_Should_Add_Valid_Employees()
        {
            // Arrange
            var employeesDto = new List<EmployeesDTO>
        {
            new EmployeesDTO
        { 
            // Example properties, adjust according to DTO definition
            PayrollNumber = "EMP001",
            Forenames = "Isl",
            Surname = "Tokhirov",
            DateOfBirth = "05/08/1997", 
            Telephone = "123456789",
            Mobile = "987654321",
            Address = "123 Main St",
            EmailHome = "isl.tkh@example.com",
            StartDate = "05/08/2024" 
        },
        new EmployeesDTO
        { 
            // Another example record
            PayrollNumber = "EMP002",
            Forenames = "Someone",
            Surname = "Surname",
            DateOfBirth = "05/08/2000", 
            Telephone = "987654321",
            Mobile = "123456789",
            Address = "456 Oak Ave",
            EmailHome = "someone@example.com",
            StartDate = "05/08/2024"
        }
        };

            var employeeEntities = _mapper.Map<List<Employees.Core.Entities.Employee>>(employeesDto);
            _mockValidationService.Setup(service => service.Validate(It.IsAny<Employees.Core.Entities.Employee>(), out It.Ref<string>.IsAny))
                                   .Returns((Employees.Core.Entities.Employee employee, string validationError) =>
                                   {
                                       validationError = null; // Set out parameter to null or any default value
                                       return true; // Always return true for validation
                                   });
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Employees.Core.Entities.Employee>()))
                           .Returns(Task.CompletedTask);

            var service = new EmployeeService(_mockRepository.Object, _mockValidationService.Object, _mapper, null /* mock error handler */);

            // Act
            var result = await service.AddRangeAsync(employeesDto);

            // Assert
            Assert.Equal(employeesDto.Count, result.TotalItems);
            Assert.Equal(employeesDto.Count, result.Q_Added);
            Assert.Empty(result.Errors);
        }

     
    }

}
