using PhucPhuongCare.CoreBusiness.Models;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface IAppointmentRepository
    {
        Task AddAppointmentAsync(Appointment appointment);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId, DateTime date);
        Task UpdateAppointmentStatusAsync(int appointmentId, CoreBusiness.Enums.AppointmentStatus status);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    }
}