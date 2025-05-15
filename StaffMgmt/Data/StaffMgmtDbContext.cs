// File: Data/StaffMgmtDbContext.cs
using Microsoft.EntityFrameworkCore;
using StaffMgmt.Models; // Namespace chứa các lớp Model đã tạo

namespace StaffMgmt.Data
{
    public class StaffMgmtDbContext : DbContext
    {
        // Constructor: Nhận DbContextOptions để cấu hình (ví dụ: chuỗi kết nối)
        // Thường được inject vào thông qua Dependency Injection trong Program.cs hoặc Startup.cs
        public StaffMgmtDbContext(DbContextOptions<StaffMgmtDbContext> options)
            : base(options)
        {
        }

        // Khai báo các DbSet tương ứng với các bảng trong database
        // Tên thuộc tính thường là tên bảng ở dạng số nhiều
        public DbSet<Province> Provinces { get; set; } = null!; // Dùng "= null!" để tránh cảnh báo NRTs
        public DbSet<District> Districts { get; set; } = null!;
        public DbSet<Ward> Wards { get; set; } = null!;
        public DbSet<Ethnicity> Ethnicities { get; set; } = null!;   // Nếu dùng bảng Ethnicities
        public DbSet<Occupation> Occupations { get; set; } = null!;   // Nếu dùng bảng Occupations
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Certificate> Certificates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gọi phương thức cơ sở trước (quan trọng)
            base.OnModelCreating(modelBuilder);

            // --- Cấu hình UNIQUE constraints cho Employee ---
            // Đảm bảo IdentityCardNumber là duy nhất (nếu không phải là NULL)
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.IdentityCardNumber)
                .IsUnique();

            // Đảm bảo PhoneNumber là duy nhất (nếu không phải là NULL)
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.PhoneNumber)
                .IsUnique();

            // --- Cấu hình Delete Behavior để khớp với Database Schema ---
            // Cấu hình mối quan hệ Tỉnh - Huyện (Cascade Delete)
            modelBuilder.Entity<District>()
                .HasOne(d => d.Province)         // Một District có một Province
                .WithMany(p => p.Districts)      // Một Province có nhiều District
                .HasForeignKey(d => d.ProvinceId)// Khóa ngoại là ProvinceId
                .OnDelete(DeleteBehavior.Cascade); // Hành vi xóa: Cascade (khớp với DB)

            // Cấu hình mối quan hệ Huyện - Xã (Cascade Delete)
            modelBuilder.Entity<Ward>()
                .HasOne(w => w.District)         // Một Ward có một District
                .WithMany(d => d.Wards)          // Một District có nhiều Ward
                .HasForeignKey(w => w.DistrictId)// Khóa ngoại là DistrictId
                .OnDelete(DeleteBehavior.Cascade); // Hành vi xóa: Cascade (khớp với DB)

            // Cấu hình mối quan hệ Employee - Văn bằng (Cascade Delete)
            modelBuilder.Entity<Certificate>()
               .HasOne(c => c.Employee)         // Một Certificate thuộc về một Employee
               .WithMany(e => e.Certificates)   // Một Employee có nhiều Certificate
               .HasForeignKey(c => c.EmployeeId)// Khóa ngoại là EmployeeId
               .OnDelete(DeleteBehavior.Cascade); // Hành vi xóa: Cascade (khớp với DB)

            // --- Cấu hình Delete Behavior cho các FK trong Employee trỏ đến Address và danh mục khác ---
            // Hành vi: Restrict (tương đương NO ACTION trong SQL Server) để ngăn xóa cha nếu còn con
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Province)
                .WithMany(p => p.Employees) // Sử dụng navigation property đã thêm vào Province model
                .HasForeignKey(e => e.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Province nếu còn Employee

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.District)
                .WithMany(d => d.Employees) // Sử dụng navigation property đã thêm vào District model
                .HasForeignKey(e => e.DistrictId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa District nếu còn Employee

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Ward)
                .WithMany(w => w.Employees) // Sử dụng navigation property đã thêm vào Ward model
                .HasForeignKey(e => e.WardId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Ward nếu còn Employee

            // Tương tự cho Ethnicity và Occupation nếu dùng bảng riêng và muốn hành vi Restrict
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Ethnicity)
                .WithMany(et => et.Employees)
                .HasForeignKey(e => e.EthnicityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
               .HasOne(e => e.Occupation)
               .WithMany(o => o.Employees)
               .HasForeignKey(e => e.OccupationId)
               .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình FK IssuerProvinceId trong Certificate là Restrict
            modelBuilder.Entity<Certificate>()
               .HasOne(c => c.IssuerProvince)
               .WithMany(p => p.IssuedCertificates) // Sử dụng navigation property đã thêm vào Province model
               .HasForeignKey(c => c.IssuerProvinceId)
               .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Tỉnh cấp nếu còn Văn bằng tham chiếu

            // --- (Tùy chọn) Seed Data: Thêm dữ liệu ban đầu vào database ---
            // modelBuilder.Entity<Province>().HasData(
            //     new Province { Id = 1, Name = "Hà Nội" },
            //     new Province { Id = 2, Name = "Hồ Chí Minh" }
            //     // Thêm các tỉnh khác...
            // );
            // // Tương tự cho các bảng khác nếu cần
        }
    }
}