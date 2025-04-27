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
    public class UpdateByPrimaryKeyTest
    {
        private readonly TestDbContext _dbContext;

        public UpdateByPrimaryKeyTest(TestDbContextFixture dbContextFixture)
        {
            _dbContext = dbContextFixture.TestDbContext;
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsSimpleAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.UpdateByPrimaryKey(entity.Id, new (Expression<Func<TestParentEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue),
                (x => x.ValueObject, newValueObject)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
            Assert.Equal(newValueObject.StrValue, result.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result.ValueObject.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsSimpleAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.TestParents.UpdateByPrimaryKey(entity.Id, new (Expression<Func<TestParentEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue),
                (x => x.ValueObject, newValueObject)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
            Assert.Equal(newValueObject.StrValue, result.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result.ValueObject.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsSimpleAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.UpdateByPrimaryKey(entity.Id, new (Expression<Func<TestParentEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue),
                (x => x.ValueObject, newValueObject)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
            Assert.Equal(newValueObject.StrValue, result.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result.ValueObject.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsSimpleAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestParentEntity>();

            _dbContext.TestParents.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.TestParents.UpdateByPrimaryKey(entity.Id, new (Expression<Func<TestParentEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue),
                (x => x.ValueObject, newValueObject)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestParents.FindAsync(entity.Id);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
            Assert.Equal(newValueObject.StrValue, result.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result.ValueObject.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsCompositeAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.UpdateByPrimaryKey(new { entity.Id1, entity.Id2 }, new (Expression<Func<TestCompositeEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsCompositeAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.TestComposites.UpdateByPrimaryKey(new { entity.Id1, entity.Id2 }, new (Expression<Func<TestCompositeEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsCompositeAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.UpdateByPrimaryKey(new { entity.Id1, entity.Id2 }, new (Expression<Func<TestCompositeEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
        }

        [Fact]
        public async void UpdateByPrimaryKey_WhenKeyIsCompositeAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
        {
            // Arrange
            var entity = ClassGenerator.Generate<TestCompositeEntity>();

            _dbContext.TestComposites.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.UntrackEntity(entity);

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.TestComposites.UpdateByPrimaryKey(new { entity.Id1, entity.Id2 }, new (Expression<Func<TestCompositeEntity, object>> property, object? value)[]
            {
                (x => x.StrValue, newStrValue),
                (x => x.IntValue, newIntValue)
            });

            await _dbContext.SaveChangesAsync();
            var result = await _dbContext.TestComposites.FindAsync(entity.Id1, entity.Id2);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(newStrValue, result.StrValue);
            Assert.Equal(newIntValue, result.IntValue);
        }
    }
}
