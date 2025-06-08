using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PatientProfilesUseCases
{
    public class ViewMyProfileUseCase : IViewMyProfileUseCase
    {
        private readonly IPatientProfileRepository _profileRepository;

        public ViewMyProfileUseCase(IPatientProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<PatientProfile?> ExecuteAsync(string userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
            if (profile == null)
            {
                // Nếu người dùng chưa có hồ sơ, trả về một hồ sơ mới, rỗng
                return new PatientProfile { ApplicationUserId = userId };
            }
            return profile;
        }
    }
}