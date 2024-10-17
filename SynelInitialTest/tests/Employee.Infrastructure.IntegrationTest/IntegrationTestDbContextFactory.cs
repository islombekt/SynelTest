using Employees.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.IntegrationTest
{
    // Configure the test database
    public class IntegrationTestDbContextFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext configuration if it exists
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                // Use a test-specific SQL Server connection string
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(GetTestConnectionString());
                });

                // Build the service provider
                var serviceProvider = services.BuildServiceProvider();

                // Ensure the database is created
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.EnsureDeleted(); // Clear the database first
                    dbContext.Database.EnsureCreated(); // Create a fresh database for testing
                }
            });
        }

        private string GetTestConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<IntegrationTestDbContextFactory>()
                .Build();

            return configuration.GetConnectionString("EmployeeConnectionString") ??
                   "Your fallback connection string here";
        }
    }


}
