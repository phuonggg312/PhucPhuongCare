using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public interface IAdminDeleteDoctorUseCase
    {
        Task ExecuteAsync(int doctorId);
    }
}