using PhucPhuongCare.UseCases.ViewModels; // Thêm using này
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public interface IAdminViewAllAppointmentsUseCase
    {
        Task<IEnumerable<AppointmentViewModel>> ExecuteAsync();
    }
}