using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SchedulesUseCases
{
    public interface IAdminDeleteScheduleUseCase
    {
        Task ExecuteAsync(int scheduleId);
    }
}