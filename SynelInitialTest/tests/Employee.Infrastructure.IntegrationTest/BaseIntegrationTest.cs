using Employees.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.IntegrationTest
{
    // To clean database in each test, and rollback changes
    public class BaseIntegrationTest : IClassFixture<IntegrationTestDbContextFactory>, IDisposable
    {
        protected readonly ApplicationDbContext _dbContext;
        private readonly IServiceScope _scope;
        private readonly IDbContextTransaction _transaction;

        public BaseIntegrationTest(IntegrationTestDbContextFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Start a transaction
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            // Rollback the transaction after each test
            _transaction.Rollback();
            _transaction.Dispose();
            _scope.Dispose();
        }
    }


}
