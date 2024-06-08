

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asd123.Model
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasIndex(e => e.Code);
            });modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.Code);
            });modelBuilder.Entity<Major>(entity =>
            {
                entity.HasIndex(e => e.Code);
            });modelBuilder.Entity<Students>(entity =>
            {
                entity.HasIndex(e => e.Code);
            });modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.Code);
            });
        }
    }
}
