namespace Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities
{
    public class TestChildEntity
    {
        public int Id { get; set; }
        public string StrValue { get; set; }
        public int IntValue { get; set; }

        public int TestParentId { get; set; }
        public TestParentEntity TestParent { get; set; }
    }
}
