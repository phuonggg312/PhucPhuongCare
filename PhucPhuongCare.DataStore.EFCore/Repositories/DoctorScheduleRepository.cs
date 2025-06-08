using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly AppDbContext _context;

        public DoctorScheduleRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddScheduleAsync(DoctorSchedule schedule)
        {
            await _context.DoctorSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteScheduleAsync(int scheduleId)
        {
            var schedule = await _context.DoctorSchedules.FindAsync(scheduleId);
            if (schedule == null) return;

            _context.DoctorSchedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }

        // Sửa lại hàm có sẵn để nó hoạt động
        public async Task<IEnumerable<DoctorSchedule>> GetSchedulesByDoctorAsync(int doctorId)
        {
            return await _context.DoctorSchedules
                .Where(s => s.DoctorId == doctorId)
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        }
    }
}