using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities.ValueObjects;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities
{
    public class TestParentEntity
    {
        public int Id { get; set; }
        public string StrValue { get; set; }
        public int IntValue { get; set; }
        public TestValueObject ValueObject { get; set; }

        public ICollection<TestChildEntity> TestChildren { get; set; }
    }
}
