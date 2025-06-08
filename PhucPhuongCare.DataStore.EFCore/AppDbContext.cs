using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // THÊM = default!; VÀO TẤT CẢ CÁC DÒNG DbSet
    public DbSet<Specialty> Specialties { get; set; } = default!;
    public DbSet<Doctor> Doctors { get; set; } = default!;
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; } = default!;
    public DbSet<TimeSlot> TimeSlots { get; set; } = default!;
    public DbSet<Appointment> Appointments { get; set; } = default!;
    public DbSet<PatientProfile> PatientProfiles { get; set; } = default!;

    // ... phần OnModelCreating giữ nguyên ...
}