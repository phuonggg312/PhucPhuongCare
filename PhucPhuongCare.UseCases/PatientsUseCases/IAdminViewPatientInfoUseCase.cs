using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PatientsUseCases
{
    public interface IAdminViewPatientInfoUseCase
    {
        Task<IdentityUser?> ExecuteAsync(string patientId);
    }
}