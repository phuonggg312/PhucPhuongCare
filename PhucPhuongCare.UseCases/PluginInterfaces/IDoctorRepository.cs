using PhucPhuongCare.CoreBusiness.Models;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(int specialtyId);
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
        // Thêm các phương thức khác cho việc quản lý bác sĩ nếu cần
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task AddDoctorAsync(Doctor doctor);

        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int doctorId);
    }
}