using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Data
{
    // create db context at design time for migrations
    public class EmployeeContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer("Data Source=SynelTest");
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}
