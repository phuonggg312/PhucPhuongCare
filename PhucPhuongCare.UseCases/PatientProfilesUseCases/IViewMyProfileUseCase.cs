using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PatientProfilesUseCases
{
    public interface IViewMyProfileUseCase
    {
        Task<PatientProfile?> ExecuteAsync(string userId);
    }
}