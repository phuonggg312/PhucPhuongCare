using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public class AdminAddDoctorUseCase : IAdminAddDoctorUseCase
    {
        private readonly IDoctorRepository _doctorRepository;
        public AdminAddDoctorUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task ExecuteAsync(Doctor doctor)
        {
            await _doctorRepository.AddDoctorAsync(doctor);
        }
    }
}