using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface IAdminMarkAsCompletedUseCase
    {
        Task ExecuteAsync(int appointmentId);
    }
}