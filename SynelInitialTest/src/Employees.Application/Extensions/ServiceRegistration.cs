using Employees.Application.Services.Handlers.Error;
using Employees.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Employees.Application.Extensions
{
    public static class ServiceRegistration
    {
        // registering all services used in Application project
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeValidationService, EmployeeValidationService>();
            services.AddScoped<IErrorHandler, ErrorHandler>();
           
            return services;
        }
    }
}
