using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class ViewMyAppointmentsUseCase : IViewMyAppointmentsUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ViewMyAppointmentsUseCase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> ExecuteAsync(string patientId)
        {
            // Chúng ta đã có sẵn phương thức này trong Repository
            return await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);
        }
    }
}