using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public class AdminDeleteDoctorUseCase : IAdminDeleteDoctorUseCase
    {
        private readonly IDoctorRepository _doctorRepository;

        public AdminDeleteDoctorUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task ExecuteAsync(int doctorId)
        {
            await _doctorRepository.DeleteDoctorAsync(doctorId);
        }
    }
}