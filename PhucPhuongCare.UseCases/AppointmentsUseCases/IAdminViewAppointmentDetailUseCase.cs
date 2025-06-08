using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface IAdminViewAppointmentDetailUseCase
    {
        Task<Appointment?> ExecuteAsync(int appointmentId);
    }
}