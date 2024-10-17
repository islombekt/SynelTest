using Employees.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            // Override the default database setup
            builder.ConfigureServices(services =>
            {
                // Find the DbContext in the service collection
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                // If the DbContext exists, remove it
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add DbContext with an in-memory database for testing purposes
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer("Data Source=ICT-ITOXOROV1;Database=SynelTest_IntegrationTestDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;"); 
                });

                // Build the service provider for DI
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .BuildServiceProvider();

                // Ensure the database is created
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    // Ensure the database is created
                    db.Database.EnsureCreated();
                }
            });
        }
    }

}
