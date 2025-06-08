using PhucPhuongCare.CoreBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface IViewMyAppointmentsUseCase
    {
        Task<IEnumerable<Appointment>> ExecuteAsync(string patientId);
    }
}