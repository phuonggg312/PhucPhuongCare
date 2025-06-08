using PhucPhuongCare.CoreBusiness.Models;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface IDoctorScheduleRepository
    {
        Task<IEnumerable<DoctorSchedule>> GetSchedulesByDoctorAsync(int doctorId);
        // Thêm các phương thức khác cho việc quản lý lịch làm việc nếu cần
        Task AddScheduleAsync(DoctorSchedule schedule);
        Task DeleteScheduleAsync(int scheduleId);
    }
}