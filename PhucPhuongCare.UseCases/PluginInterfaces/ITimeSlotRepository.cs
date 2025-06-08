using PhucPhuongCare.CoreBusiness.Models;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync(int doctorId, DateTime date);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);
        Task UpdateTimeSlotStatusAsync(int timeSlotId, CoreBusiness.Enums.TimeSlotStatus status);
        // Phương thức để hệ thống tự động sinh ra các TimeSlot
        Task GenerateSlotsForDoctorAsync(int doctorId, DateTime startDate, DateTime endDate);
    }
}