using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class AdminViewAllAppointmentsUseCase : IAdminViewAllAppointmentsUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AdminViewAllAppointmentsUseCase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> ExecuteAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }
    }
}