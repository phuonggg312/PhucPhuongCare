using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SchedulesUseCases
{
    public interface IAdminAddScheduleUseCase
    {
        Task ExecuteAsync(DoctorSchedule schedule);
    }
}