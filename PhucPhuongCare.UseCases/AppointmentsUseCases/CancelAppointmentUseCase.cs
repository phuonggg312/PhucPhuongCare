using PhucPhuongCare.CoreBusiness.Enums;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class CancelAppointmentUseCase : ICancelAppointmentUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CancelAppointmentUseCase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task ExecuteAsync(int appointmentId)
        {
            // Gọi đến repository để cập nhật trạng thái
            await _appointmentRepository.UpdateAppointmentStatusAsync(appointmentId, AppointmentStatus.CanceledByPatient);
        }
    }
}