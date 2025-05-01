## About
This package extends EF Core for additional functionalities such as updating and removing entities by their primary keys in one transaction without fetching them first from the database. Although EF Core provides `ExecuteUpdate` and `ExecuteDelete`, they hit the database when they are called. This package, however, allows you to create complex business logics in one transaction.

## How to Use
After installing the package and adding `Cayd.EntityFrameworkCore.Extensions` namespace to your code, you can start using the extension methods over either `DbContext` or `DbSet<T>`. They both have the same methods and work the same.

- `UpdateByPrimarKey` and `RemoveByPrimaryKey` over `DbContext`:
```csharp
using Cayd.EntityFrameworkCore.Extensions;

public void MyComplexBusinessLogic(int idToUpdate, int idToRemove)
{
    // ... do other CRUD operations / business logics

    /** For .Net 8 and above */
    dbContext.UpdateByPrimaryKey(idToUpdate, [
        (x => x.Property1, newValue1),
        (x => x.Property2, newValue2),
        ...
    ]);

    dbContext.RemoveByPrimaryKey<MyEntity>(idToRemove);

    /** For versions lower than .Net 8 */
    dbContext.UpdateByPrimaryKey(idToUpdate, new (Expression<Func<MyEntity, object>> property, object? value)[] {
        (x => x.Property1, newValue1),
        (x => x.Property2, newValue2),
        ...
    });

    dbContext.RemoveByPrimaryKey<MyEntity>(idToRemove);

    // All of them are done in one transaction
    dbContext.SaveChanges();
}
```

- `UpdateByPrimarKey` and `RemoveByPrimaryKey` over `DbSet<T>`:
```csharp
using Cayd.EntityFrameworkCore.Extensions;

public void MyComplexBusinessLogic(int idToUpdate, int idToRemove)
{
    // ... do other CRUD operations / business logics

    /** For .Net 8 and above */
    dbContext.MyEntities.UpdateByPrimaryKey(idToUpdate, [
        (x => x.Property1, newValue1),
        (x => x.Property2, newValue2),
        ...
    ]);

    dbContext.MyEntities.RemoveByPrimaryKey(idToRemove);

    /** For versions lower than .Net 8 */
    dbContext.MyEntities.UpdateByPrimaryKey(idToUpdate, new (Expression<Func<MyEntity, object>> property, object? value)[] {
        (x => x.Property1, newValue1),
        (x => x.Property2, newValue2),
        ...
    });

    dbContext.MyEntities.RemoveByPrimaryKey(idToRemove);

    // All of them are done in one transaction
    dbContext.SaveChanges();
}
```

- `BulkUpdateByPrimarKeys` and `RemoveRangeByPrimaryKeys` over `DbContext`:
```csharp
using Cayd.EntityFrameworkCore.Extensions;

public void MyComplexBusinessLogic(List<int> idsToUpdate, List<int> idsToRemove)
{
    // ... do other CRUD operations / business logics

    /** For .Net 8 and above */
    dbContext.BulkUpdateByPrimaryKeys(idsToUpdate, [
        (x => x.Property1, newValue1, false),
        (x => x.Property2, newValue2, false),
        // The third parameter controls whether the new value needs to be copied or not.
        // This is necessary for value objects. If the property is not a value object, set it to false
        (x => x.MyValueObject, newValueObject, true),
        ...
    ]);

    dbContext.RemoveRangeByPrimaryKeys<MyEntity>(idsToRemove);

    /** For versions lower than .Net 8 */
    dbContext.UpdateByPrimaryKey(idsToUpdate, new (Expression<Func<MyEntity, object>> property, object? value, bool isValueObject)[] {
        (x => x.Property1, newValue1, false),
        (x => x.Property2, newValue2, false),
        // The third parameter controls whether the new value needs to be copied or not.
        // This is necessary for value objects. If the property is not a value object, set it to false
        (x => x.MyValueObject, newValueObject, true),
        ...
    });

    dbContext.RemoveRangeByPrimaryKeys<MyEntity>(idsToRemove);

    // All of them are done in one transaction
    dbContext.SaveChanges();
}
```

- `BulkUpdateByPrimarKeys` and `RemoveRangeByPrimaryKeys` over `DbSet<T>`:
```csharp
using Cayd.EntityFrameworkCore.Extensions;

public void MyComplexBusinessLogic(List<int> idsToUpdate, List<int> idsToRemove)
{
    // ... do other CRUD operations / business logics

    /** For .Net 8 and above */
    dbContext.MyEntities.BulkUpdateByPrimaryKeys(idsToUpdate, [
        (x => x.Property1, newValue1, false),
        (x => x.Property2, newValue2, false),
        // The third parameter controls whether the new value needs to be copied or not.
        // This is necessary for value objects. If the property is not a value object, set it to false
        (x => x.MyValueObject, newValueObject, true),
        ...
    ]);

    dbContext.MyEntities.RemoveRangeByPrimaryKeys(idsToRemove);

    /** For versions lower than .Net 8 */
    dbContext.MyEntities.UpdateByPrimaryKey(idsToUpdate, new (Expression<Func<MyEntity, object>> property, object? value, bool isValueObject)[] {
        (x => x.Property1, newValue1, false),
        (x => x.Property2, newValue2, false),
        // The third parameter controls whether the new value needs to be copied or not.
        // This is necessary for value objects. If the property is not a value object, set it to false
        (x => x.MyValueObject, newValueObject, true),
        ...
    });

    dbContext.MyEntities.RemoveRangeByPrimaryKeys(idsToRemove);

    // All of them are done in one transaction
    dbContext.SaveChanges();
}
```

- `NOTE`: If your primary key is a composite key, then you have to pass the primary key as an anonymous class. All the extensions methods support composite keys.
```csharp
dbContext.UpdateByPrimaryKey(new { id1, id2 }, [
    (x => x.Property1, newValue1),
    (x => x.Property2, newValue2),
    ...
]);

dbContext.RemoveByPrimaryKey<MyEntity>(new { id1, id2 });
```

## Extensions

Method Name                                               | Functionality
----------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------
UpdateByPrimaryKey(primaryKey, propertiesAndValues)       | Updates a single entity via its primary key without fetching it from the database.
BulkUpdateByPrimaryKeys(primaryKeys, propertiesAndValues) | Updates multiple entities with the same values via their primary keys without fetching them from the database.
RemoveByPrimaryKey(primaryKey)                            | Removes a single entity via its primary key without fetching it from the database.
RemoveRangeByPrimaryKeys(primaryKeys)                     | Removes multiple entities via their primary keys without fetching them from the database.