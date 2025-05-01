using Cayd.EntityFrameworkCore.Extensions.Test.Api.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities;
using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities.ValueObjects;
using Cayd.EntityFrameworkCore.Extensions.Test.Integration.Collections.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Fixtures;
using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Helpers;
using Cayd.Test.Generators;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Integration.Extensions
{
    [Collection(nameof(TestDbContextCollection))]
    public class RemoveByPrimaryKeyTest
    {
        private readonly TestDbContext _dbContext;

        public RemoveByPrimaryKeyTest(TestDbContextFixture dbContextFixture)
        {
            _dbContext = dbContextFixture.TestDbContext;
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsSimpleAndEntityIsInChangeTrackerAndEntityIsRemovedViaDbContext_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.RemoveByPrimaryKey<TestParentEntity>(entity.Id);

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsSimpleAndEntityIsInChangeTrackerAndEntityIsRemovedViaDbSet_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.TestParents.RemoveByPrimaryKey(entity.Id);

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsSimpleAndEntityIsNotInChangeTrackerAndEntityIsRemovedViaDbContext_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            // Act
            _dbContext.RemoveByPrimaryKey<TestParentEntity>(entity.Id);

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsSimpleAndEntityIsNotInChangeTrackerAndEntityIsRemovedViaDbSet_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            // Act
            _dbContext.TestParents.RemoveByPrimaryKey(entity.Id);

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsCompositeAndEntityIsInChangeTrackerAndEntityIsRemovedViaDbContext_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.RemoveByPrimaryKey<TestCompositeEntity>(new { entity.Id1, entity.Id2 });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsCompositeAndEntityIsInChangeTrackerAndEntityIsRemovedViaDbSet_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();

            // Act
            _dbContext.TestComposites.RemoveByPrimaryKey(new { entity.Id1, entity.Id2 });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsCompositeAndEntityIsNotInChangeTrackerAndEntityIsRemovedViaDbContext_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            // Act
            _dbContext.RemoveByPrimaryKey<TestCompositeEntity>(new { entity.Id1, entity.Id2 });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void RemoveByPrimaryKey_WhenKeyIsCompositeAndEntityIsNotInChangeTrackerAndEntityIsRemovedViaDbSet_ShouldRemoveEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            // Act
            _dbContext.TestComposites.RemoveByPrimaryKey(new { entity.Id1, entity.Id2 });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Assert
            Assert.Null(result);
        }
    }
}
