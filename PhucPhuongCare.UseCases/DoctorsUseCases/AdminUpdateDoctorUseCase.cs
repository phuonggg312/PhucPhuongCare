using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public class AdminUpdateDoctorUseCase : IAdminUpdateDoctorUseCase
    {
        private readonly IDoctorRepository _doctorRepository;

        public AdminUpdateDoctorUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task ExecuteAsync(Doctor doctor)
        {
            await _doctorRepository.UpdateDoctorAsync(doctor);
        }
    }
}