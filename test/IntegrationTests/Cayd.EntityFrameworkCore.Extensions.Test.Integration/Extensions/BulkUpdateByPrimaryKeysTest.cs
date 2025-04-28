using Cayd.EntityFrameworkCore.Extensions.Test.Api.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities;
using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities.ValueObjects;
using Cayd.EntityFrameworkCore.Extensions.Test.Integration.Collections.DbContexts;
using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Fixtures;
using Cayd.Test.Generators;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Integration.Extensions
{
    [Collection(nameof(TestDbContextCollection))]
    public class BulkUpdateByPrimaryKeysTest
    {
        private readonly TestDbContext _dbContext;

        public BulkUpdateByPrimaryKeysTest(TestDbContextFixture dbContextFixture)
        {
            _dbContext = dbContextFixture.TestDbContext;
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsSimpleAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestParentEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false),
                (x => x.ValueObject, newValueObject, true)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);
            Assert.Equal(newValueObject.StrValue, result1.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result1.ValueObject.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
            Assert.Equal(newValueObject.StrValue, result2.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result2.ValueObject.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsSimpleAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.TestParents.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestParentEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false),
                (x => x.ValueObject, newValueObject, true)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);
            Assert.Equal(newValueObject.StrValue, result1.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result1.ValueObject.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
            Assert.Equal(newValueObject.StrValue, result2.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result2.ValueObject.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsSimpleAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestParentEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false),
                (x => x.ValueObject, newValueObject, true)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);
            Assert.Equal(newValueObject.StrValue, result1.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result1.ValueObject.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
            Assert.Equal(newValueObject.StrValue, result2.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result2.ValueObject.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsSimpleAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
        {
            // Arrange
            var entity1 = ClassGenerator.Generate<TestParentEntity>();
            var entity2 = ClassGenerator.Generate<TestParentEntity>();
            List<int> ids = new List<int>() { entity1.Id, entity2.Id };

            _dbContext.TestParents.Add(entity1);
            _dbContext.TestParents.Add(entity2);
            await _dbContext.SaveChangesAsync();

            var newStrValue = "123456789";
            var newIntValue = 0;
            var newValueObject = new TestValueObject()
            {
                StrValue = "qweasdzxc",
                IntValue = -1
            };

            // Act
            _dbContext.TestParents.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestParentEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false),
                (x => x.ValueObject, newValueObject, true)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestParents.FindAsync(entity1.Id);
            var result2 = await _dbContext.TestParents.FindAsync(entity2.Id);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);
            Assert.Equal(newValueObject.StrValue, result1.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result1.ValueObject.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
            Assert.Equal(newValueObject.StrValue, result2.ValueObject.StrValue);
            Assert.Equal(newValueObject.IntValue, result2.ValueObject.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsCompositeAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
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

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestCompositeEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsCompositeAndEntityIsInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
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

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.TestComposites.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestCompositeEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsCompositeAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbContext_ShouldUpdateEntity()
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

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestCompositeEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
        }

        [Fact]
        public async void BulkUpdateByPrimaryKeys_WhenKeyIsCompositeAndEntityIsNotInChangeTrackerAndEntityIsUpdatedViaDbSet_ShouldUpdateEntity()
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

            var newStrValue = "123456789";
            var newIntValue = 0;

            // Act
            _dbContext.TestComposites.BulkUpdateByPrimaryKeys(ids, new (Expression<Func<TestCompositeEntity, object>> property, object? value, bool isValueObject)[]
            {
                (x => x.StrValue, newStrValue, false),
                (x => x.IntValue, newIntValue, false)
            });

            await _dbContext.SaveChangesAsync();
            var result1 = await _dbContext.TestComposites.FindAsync(entity1.Id1, entity1.Id2);
            var result2 = await _dbContext.TestComposites.FindAsync(entity2.Id1, entity2.Id2);

            // Arrange
            Assert.NotNull(result1);
            Assert.Equal(newStrValue, result1.StrValue);
            Assert.Equal(newIntValue, result1.IntValue);

            Assert.NotNull(result2);
            Assert.Equal(newStrValue, result2.StrValue);
            Assert.Equal(newIntValue, result2.IntValue);
        }
    }
}
