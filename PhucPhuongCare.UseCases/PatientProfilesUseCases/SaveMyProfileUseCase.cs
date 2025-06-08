using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PatientProfilesUseCases
{
    public class SaveMyProfileUseCase : ISaveMyProfileUseCase
    {
        private readonly IPatientProfileRepository _profileRepository;

        public SaveMyProfileUseCase(IPatientProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task ExecuteAsync(PatientProfile profile)
        {
            await _profileRepository.SaveProfileAsync(profile);
        }
    }
}