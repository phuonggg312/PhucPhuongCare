using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface IBookAppointmentUseCase
    {
        Task<bool> ExecuteAsync(int doctorId, int timeSlotId, string patientId, string reasonForVisit);
    }
}