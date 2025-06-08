using PhucPhuongCare.CoreBusiness.Enums;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class AdminMarkAsCompletedUseCase : IAdminMarkAsCompletedUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AdminMarkAsCompletedUseCase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task ExecuteAsync(int appointmentId)
        {
            await _appointmentRepository.UpdateAppointmentStatusAsync(appointmentId, AppointmentStatus.Completed);
        }
    }
}