using StudentPortalAPI.Models;

namespace PortalNotas.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.ProfessorProfile)
                    .WithOne(p => p.User)
                    .HasForeignKey<Professor>(p => p.UserId);

                entity.HasOne(u => u.StudentProfile)
                    .WithOne(sp => sp.User)
                    .HasForeignKey<Student>(sp => sp.UserId);

                entity.HasIndex(u => u.Username)
                    .IsUnique();

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });


            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.ProfessorId);

            modelBuilder.Entity<CourseEnrollment>(ce =>
            {
                ce.HasKey(ce => new { ce.CourseId, ce.StudentId });

                ce.HasOne(ce => ce.Course)
                    .WithMany(c => c.CourseEnrollments)
                    .HasForeignKey(ce => ce.CourseId);

                ce.HasOne(ce => ce.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(ce => ce.StudentId);
            });
        }
    }
}
