using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public interface IAdminUpdateDoctorUseCase
    {
        Task ExecuteAsync(Doctor doctor);
    }
}