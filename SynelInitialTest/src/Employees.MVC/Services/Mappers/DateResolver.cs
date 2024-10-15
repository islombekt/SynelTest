using AutoMapper;
using Employees.MVC.Models.DTOs;
using Employees.MVC.Models.Entities;
using System.Globalization;

namespace Employees.MVC.Services.Mappers
{
    public class DateResolver : IValueResolver<EmployeesDTO, Employee, DateTime>
    {
        public DateTime Resolve(EmployeesDTO source, Employee destination, DateTime destMember, ResolutionContext context)
        {
            DateTime parsedDate;

            // Try parsing the date from string using multiple formats
            if (DateTime.TryParseExact(source.DateOfBirth, new[] { "dd/MM/yyyy", "d/M/yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }

            // If parsing fails, you can either throw an exception or return a default value
            throw new Exception("Invalid date format for DateOfBirth.");
        }
    }
}
