using Cayd.EntityFrameworkCore.Extensions.Test.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Api.DbContexts
{
    public class TestDbContext : DbContext
    {
        public DbSet<TestParentEntity> TestParents { get; set; }
        public DbSet<TestChildEntity> TestChildren { get; set; }
        public DbSet<TestCompositeEntity> TestComposites { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestCompositeEntity>()
                .HasKey(e => new { e.Id1, e.Id2 });

            modelBuilder.Entity<TestParentEntity>()
                .HasMany(p => p.TestChildren)
                .WithOne(c => c.TestParent)
                .HasForeignKey(c => c.TestParentId);

#if NET8_0_OR_GREATER
            modelBuilder.Entity<TestParentEntity>()
                .ComplexProperty(p => p.ValueObject);
#else
            modelBuilder.Entity<TestParentEntity>()
                .OwnsOne(p => p.ValueObject);
#endif
        }
    }
}
