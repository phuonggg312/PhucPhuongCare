using PhucPhuongCare.CoreBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SchedulesUseCases
{
    public interface IAdminViewSchedulesUseCase
    {
        Task<IEnumerable<DoctorSchedule>> ExecuteAsync(int doctorId);
    }
}