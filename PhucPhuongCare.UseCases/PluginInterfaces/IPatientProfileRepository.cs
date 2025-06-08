using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface IPatientProfileRepository
    {
        Task<PatientProfile?> GetProfileByUserIdAsync(string userId);
        Task SaveProfileAsync(PatientProfile profile);
    }
}