using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface ICancelAppointmentUseCase
    {
        Task ExecuteAsync(int appointmentId);
    }
}