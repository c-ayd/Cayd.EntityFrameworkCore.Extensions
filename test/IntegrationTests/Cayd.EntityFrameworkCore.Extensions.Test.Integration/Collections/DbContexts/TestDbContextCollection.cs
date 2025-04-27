using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Fixtures;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Integration.Collections.DbContexts
{
    [CollectionDefinition(nameof(TestDbContextCollection))]
    public class TestDbContextCollection : IClassFixture<TestDbContextFixture>
    {
    }
}
