using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class AdminViewAppointmentDetailUseCase : IAdminViewAppointmentDetailUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AdminViewAppointmentDetailUseCase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment?> ExecuteAsync(int appointmentId)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        }
    }
}