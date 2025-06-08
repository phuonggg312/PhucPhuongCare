using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Models;

namespace PhucPhuongCare.DataStore.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình để xử lý cảnh báo về kiểu decimal
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18, 2)");

            // Cấu hình để xử lý lỗi Multiple Cascade Paths
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Thay đổi quy tắc xóa

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.TimeSlot)
                .WithMany()
                .HasForeignKey(a => a.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict); // Thay đổi quy tắc xóa


            // Cấu hình dữ liệu mẫu (seeding data)
            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { Id = 1, Name = "Tim mạch", Description = "Chuyên khoa khám và điều trị các bệnh về tim mạch.", IsActive = true },
                new Specialty { Id = 2, Name = "Nhi khoa", Description = "Chuyên khoa chăm sóc sức khỏe cho trẻ em.", IsActive = true },
                new Specialty { Id = 3, Name = "Nha khoa", Description = "Chuyên khoa răng, hàm, mặt.", IsActive = true }
            );
            // Thêm dữ liệu mẫu cho Bác sĩ
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FullName = "BS.CKI Nguyễn Thị An",
                    SpecialtyId = 1, // Tim mạch
                    Degree = "Bác sĩ Chuyên khoa I",
                    Bio = "Hơn 10 năm kinh nghiệm trong lĩnh vực nội tim mạch. Tận tâm và chuyên nghiệp.",
                    ApplicationUserId = Guid.NewGuid().ToString() // ID giả lập, sẽ được thay thế sau
                },
                new Doctor
                {
                    Id = 2,
                    FullName = "ThS.BS Trần Văn Bình",
                    SpecialtyId = 1, // Tim mạch
                    Degree = "Thạc sĩ, Bác sĩ",
                    Bio = "Chuyên gia về các bệnh lý mạch vành và cao huyết áp. Từng tu nghiệp tại Singapore.",
                    ApplicationUserId = Guid.NewGuid().ToString() // ID giả lập
                },
                new Doctor
                {
                    Id = 3,
                    FullName = "BS.CKII Lê Thị Cúc",
                    SpecialtyId = 2, // Nhi khoa
                    Degree = "Bác sĩ Chuyên khoa II",
                    Bio = "Rất có kinh nghiệm với các bệnh lý ở trẻ sơ sinh và trẻ nhỏ.",
                    ApplicationUserId = Guid.NewGuid().ToString() // ID giả lập
                }
            );
        }
    }
}
