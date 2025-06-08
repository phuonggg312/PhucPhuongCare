using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PatientProfilesUseCases
{
    public interface ISaveMyProfileUseCase
    {
        Task ExecuteAsync(PatientProfile profile);
    }
}