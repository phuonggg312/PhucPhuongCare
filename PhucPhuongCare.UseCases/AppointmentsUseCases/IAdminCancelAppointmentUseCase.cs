using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface IAdminCancelAppointmentUseCase
    {
        Task ExecuteAsync(int appointmentId);
    }
}