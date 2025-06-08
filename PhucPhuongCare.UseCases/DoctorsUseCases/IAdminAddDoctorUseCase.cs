using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public interface IAdminAddDoctorUseCase
    {
        Task ExecuteAsync(Doctor doctor);
    }
}