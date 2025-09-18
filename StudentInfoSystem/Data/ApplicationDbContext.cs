using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Marks).HasColumnType("decimal(5,2)");
                entity.Property(e => e.Grade).HasMaxLength(2);
            });

            // Seed initial data
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Alice Johnson", Age = 20, Marks = 85.5m, Grade = "A" },
                new Student { Id = 2, Name = "Bob Smith", Age = 19, Marks = 78.0m, Grade = "B" },
                new Student { Id = 3, Name = "Carol Davis", Age = 21, Marks = 92.5m, Grade = "A" },
                new Student { Id = 4, Name = "David Wilson", Age = 20, Marks = 65.0m, Grade = "C" },
                new Student { Id = 5, Name = "Emma Brown", Age = 22, Marks = 88.5m, Grade = "A" },
                new Student { Id = 6, Name = "Frank Miller", Age = 19, Marks = 72.0m, Grade = "B" },
                new Student { Id = 7, Name = "Grace Taylor", Age = 20, Marks = 95.0m, Grade = "A" },
                new Student { Id = 8, Name = "Henry Anderson", Age = 21, Marks = 58.5m, Grade = "D" },
                new Student { Id = 9, Name = "Ivy Garcia", Age = 19, Marks = 82.0m, Grade = "B" },
                new Student { Id = 10, Name = "Jack Martinez", Age = 20, Marks = 76.5m, Grade = "B" }
            );
        }
    }
}
