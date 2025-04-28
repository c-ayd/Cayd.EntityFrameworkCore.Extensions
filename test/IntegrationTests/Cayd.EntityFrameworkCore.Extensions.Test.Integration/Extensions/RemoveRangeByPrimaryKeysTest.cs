using Cayd.EntityFrameworkCore.Extensions.Test.Api.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities;
using Cayd.EntityFrameworkCore.Extensions.Test.Integration.Collections.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Fixtures;
using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Helpers;
using Cayd.Test.Generators;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Integration.Extensions
{
    [Collection(nameof(TestDbContextCollection))]
    public class RemoveRangeByPrimaryKeysTest
    {
        private readonly TestDbContext _dbContext;

        public RemoveRangeByPrimaryKeysTest(TestDbContextFixture dbContextFixture)
        {
            _dbContext = dbContextFixture.TestDbContext;
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsSimpleAndEntitiesAreInChangeTrackerAndEntitiesAreRemovedViaDbContext_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.RemoveRangeByPrimaryKeys<TestParentEntity>(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsSimpleAndEntitiesAreInChangeTrackerAndEntitiesAreRemovedViaDbSet_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.TestParents.RemoveRangeByPrimaryKeys(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsSimpleAndEntitiesAreNotInChangeTrackerAndEntitiesAreRemovedViaDbContext_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity1);
            _dbContext.UntrackEntity(entity2);

            // Act
            _dbContext.RemoveRangeByPrimaryKeys<TestParentEntity>(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsSimpleAndEntitiesAreNotInChangeTrackerAndEntitiesAreRemovedViaDbSet_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity1);
            _dbContext.UntrackEntity(entity2);

            // Act
            _dbContext.TestParents.RemoveRangeByPrimaryKeys(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsCompositeAndEntitiesAreInChangeTrackerAndEntitiesAreRemovedViaDbContext_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestCompositeEntity>();
            var entity2 = ClassGenerator.Generate<TestCompositeEntity>();
            List<object> ids = new List<object>()
            {
                new { entity1.Id1, entity1.Id2 },
                new { entity2.Id1, entity2.Id2 }
            };

            _dbContext.TestComposites.Add(entity1);
            _dbContext.TestComposites.Add(entity2);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.RemoveRangeByPrimaryKeys<TestCompositeEntity>(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsCompositeAndEntitiesAreInChangeTrackerAndEntitiesAreRemovedViaDbSet_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestCompositeEntity>();
            var entity2 = ClassGenerator.Generate<TestCompositeEntity>();
            List<object> ids = new List<object>()
            {
                new { entity1.Id1, entity1.Id2 },
                new { entity2.Id1, entity2.Id2 }
            };

            _dbContext.TestComposites.Add(entity1);
            _dbContext.TestComposites.Add(entity2);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.TestComposites.RemoveRangeByPrimaryKeys(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsCompositeAndEntitiesAreNotInChangeTrackerAndEntitiesAreRemovedViaDbContext_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestCompositeEntity>();
            var entity2 = ClassGenerator.Generate<TestCompositeEntity>();
            List<object> ids = new List<object>()
            {
                new { entity1.Id1, entity1.Id2 },
                new { entity2.Id1, entity2.Id2 }
            };

            _dbContext.TestComposites.Add(entity1);
            _dbContext.TestComposites.Add(entity2);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity1);
            _dbContext.UntrackEntity(entity2);

            // Act
            _dbContext.RemoveRangeByPrimaryKeys<TestCompositeEntity>(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void RemoveRangeByPrimaryKeys_WhenKeyIsCompositeAndEntitiesAreNotInChangeTrackerAndEntitiesAreRemovedViaDbSet_ShouldRemoveEntities()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestCompositeEntity>();
            var entity2 = ClassGenerator.Generate<TestCompositeEntity>();
            List<object> ids = new List<object>()
            {
                new { entity1.Id1, entity1.Id2 },
                new { entity2.Id1, entity2.Id2 }
            };

            _dbContext.TestComposites.Add(entity1);
            _dbContext.TestComposites.Add(entity2);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity1);
            _dbContext.UntrackEntity(entity2);

            // Act
            _dbContext.TestComposites.RemoveRangeByPrimaryKeys(ids);

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.Null(result1);
            Assert.Null(result2);
        }
    }
}
