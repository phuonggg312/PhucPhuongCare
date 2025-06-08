using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Enums;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly AppDbContext _context;
        public TimeSlotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync(int doctorId, DateTime date)
        {
            return await _context.TimeSlots
                .Where(t => t.DoctorId == doctorId &&
                            t.SlotDateTime.Date == date.Date &&
                            t.Status == TimeSlotStatus.Available)
                .OrderBy(t => t.SlotDateTime)
                .ToListAsync();
        }

        // =============================================
        // ===== BẮT ĐẦU TRIỂN KHAI LOGIC SINH SLOT =====
        // =============================================
        public async Task GenerateSlotsForDoctorAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var doctorSchedules = await _context.DoctorSchedules
                                                .Where(s => s.DoctorId == doctorId && s.IsActive)
                                                .ToListAsync();

            if (!doctorSchedules.Any()) return; // Bác sĩ không có lịch làm việc

            var newSlots = new List<TimeSlot>();

            // Lặp qua từng ngày từ ngày bắt đầu đến ngày kết thúc
            for (var day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
            {
                // Tìm lịch làm việc cho ngày trong tuần tương ứng (e.g., Thứ 2, Thứ 3)
                var scheduleForDay = doctorSchedules.FirstOrDefault(s => s.DayOfWeek == day.DayOfWeek);

                if (scheduleForDay != null)
                {
                    // Lặp từ giờ bắt đầu đến giờ kết thúc của lịch
                    for (var time = scheduleForDay.StartTime; time < scheduleForDay.EndTime; time = time.Add(TimeSpan.FromMinutes(scheduleForDay.SlotDurationMinutes)))
                    {
                        var slotDateTime = day.Add(time);

                        // KIỂM TRA QUAN TRỌNG: Chỉ thêm nếu slot này chưa hề tồn tại trong DB
                        bool slotExists = await _context.TimeSlots.AnyAsync(ts => ts.DoctorId == doctorId && ts.SlotDateTime == slotDateTime);
                        if (!slotExists)
                        {
                            newSlots.Add(new TimeSlot
                            {
                                DoctorId = doctorId,
                                SlotDateTime = slotDateTime,
                                Status = TimeSlotStatus.Available
                            });
                        }
                    }
                }
            }

            if (newSlots.Any())
            {
                await _context.TimeSlots.AddRangeAsync(newSlots);
                await _context.SaveChangesAsync();
            }
        }
        // ===== KẾT THÚC TRIỂN KHAI LOGIC SINH SLOT =====

        public async Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId)
        {
            return await _context.TimeSlots.FindAsync(timeSlotId);
        }

        public async Task UpdateTimeSlotStatusAsync(int timeSlotId, TimeSlotStatus status)
        {
            var slot = await _context.TimeSlots.FindAsync(timeSlotId);
            if (slot != null)
            {
                slot.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}