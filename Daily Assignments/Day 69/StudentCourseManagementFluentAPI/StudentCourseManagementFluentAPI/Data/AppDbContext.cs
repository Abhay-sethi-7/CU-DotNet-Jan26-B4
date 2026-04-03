using Microsoft.EntityFrameworkCore;
using StudentCourseManagementFluentAPI.Models;

namespace StudentCourseManagementFluentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student Config
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired();

                entity.HasIndex(e => e.Email)
                      .IsUnique();
            });

            // Course Config
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                      .IsRequired();
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourses");

                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity.HasOne(sc => sc.Student)
                      .WithMany(s => s.StudentCourses)
                      .HasForeignKey(sc => sc.StudentId);

                entity.HasOne(sc => sc.Course)
                      .WithMany(c => c.StudentCourses)
                      .HasForeignKey(sc => sc.CourseId);
            });
        }
    }
}
