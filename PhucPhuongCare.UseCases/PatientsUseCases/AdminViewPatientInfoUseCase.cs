using Microsoft.AspNetCore.Identity;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PatientsUseCases
{
    public class AdminViewPatientInfoUseCase : IAdminViewPatientInfoUseCase
    {
        private readonly IUserRepository _userRepository;
        public AdminViewPatientInfoUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityUser?> ExecuteAsync(string patientId)
        {
            return await _userRepository.GetUserByIdAsync(patientId);
        }
    }
}