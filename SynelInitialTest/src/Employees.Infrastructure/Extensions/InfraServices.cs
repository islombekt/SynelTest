using Employees.Core.Repositories;
using Employees.Infrastructure.Data;
using Employees.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString(
                        "EmployeeConnectionString")));
           
            servicesCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return servicesCollection;
        }
    }
}
