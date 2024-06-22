
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asd123.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public  DbSet<Lop> Lops { get; set; }
        public  DbSet<Khoa> Khoas { get; set; }
        public  DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public  DbSet<Diem> Diems { get; set; }
        public  DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
