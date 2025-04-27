using Cayd.EntityFrameworkCore.Extensions.Test.Api.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Utility.Fixtures
{
    public class TestDbContextFixture : IAsyncLifetime
    {
        public TestDbContext TestDbContext { get; private set; }

        public async Task InitializeAsync()
        {
#if NET8_0_OR_GREATER
            var connectionString = ConfigurationHelper.GetOption<string>("ConnectionStrings:Test8.0")!;
#else
            var connectionString = ConfigurationHelper.GetOption<string>("ConnectionStrings:Test6.0")!;
#endif
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseNpgsql(connectionString, options =>
                {
                    options.MigrationsAssembly("Cayd.EntityFrameworkCore.Extensions.Test.Api");
                }).Options;

            TestDbContext = new TestDbContext(options);

            if (await TestDbContext.Database.CanConnectAsync())
            {
                await TestDbContext.Database.EnsureDeletedAsync();
            }

            await TestDbContext.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await TestDbContext.Database.EnsureDeletedAsync();
            await TestDbContext.DisposeAsync();
        }
    }
}
