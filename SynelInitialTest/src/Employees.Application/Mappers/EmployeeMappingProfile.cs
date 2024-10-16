using AutoMapper;
using Employees.Application.Responses;
using Employees.Core.Entities;
namespace Employees.Application.Mappers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            // map string date members separately to DateTime
            CreateMap<EmployeesDTO, Employee>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom<DateResolver>()) 
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom<DateResolver>());
            CreateMap<Employee, EmployeesDTO>().ReverseMap();
        }
    }
}
