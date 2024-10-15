using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Employees.Web.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TContext>();
                try
                {
                    
                    var retry = Policy.Handle<SqlException>().WaitAndRetry(
                        retryCount: 5, sleepDurationProvider: retrAttempt => TimeSpan.FromSeconds(Math.Pow(retrAttempt, 2))
                        
                        );
                    context.Database.Migrate();
                    Console.WriteLine("--> Migration applied");

                }
                catch (Exception ex)
                {
                   Console.WriteLine(ex.InnerException);
                }
            }
            return host;
        }
    }
}
