using AutoMapper;
using Employees.Application.Responses;
using Employees.Core.Entities;
using System.Globalization;

namespace Employees.Application.Mappers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeesDTO, Employee>()
          .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom<DateResolver>())
          .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src =>
              DateTime.ParseExact(src.StartDate, new[] { "dd/MM/yyyy", "d/M/yyyy" },
              CultureInfo.InvariantCulture, DateTimeStyles.None)));
            CreateMap<Employee, EmployeesDTO>().ReverseMap();
        }
    }
}
