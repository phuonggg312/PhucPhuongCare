using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Enums;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                                 .Include(a => a.Doctor)
                                 .Include(a => a.TimeSlot)
                                 .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId, DateTime date)
        {
            return await _context.Appointments
                                 .Where(a => a.DoctorId == doctorId && a.TimeSlot.SlotDateTime.Date == date.Date)
                                 .Include(a => a.TimeSlot)
                                 .ToListAsync();
        }

        // =============================================
        // SỬA LỖI Ở HÀM NÀY
        // =============================================
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId)
        {
            return await _context.Appointments
                                 .Where(a => a.PatientId == patientId && a.TimeSlot != null) // << THÊM ĐIỀU KIỆN LỌC NULL
                                 .Include(a => a.Doctor)
                                 .ThenInclude(d => d.Specialty)
                                 .Include(a => a.TimeSlot)
                                 .OrderByDescending(a => a.TimeSlot.SlotDateTime)
                                 .ToListAsync();
        }

        // =============================================
        // SỬA LUÔN HÀM NÀY ĐỂ TRÁNH LỖI TƯƠNG TỰ
        // =============================================
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Where(a => a.TimeSlot != null) // << THÊM ĐIỀU KIỆN LỌC NULL
                .Include(a => a.Doctor)
                .Include(a => a.TimeSlot)
                .OrderByDescending(a => a.TimeSlot.SlotDateTime)
                .ToListAsync();
        }

        public async Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = status;
                appointment.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        
    }
}