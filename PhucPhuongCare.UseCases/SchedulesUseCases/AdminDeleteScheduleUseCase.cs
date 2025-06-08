using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SchedulesUseCases
{
    public class AdminDeleteScheduleUseCase : IAdminDeleteScheduleUseCase
    {
        private readonly IDoctorScheduleRepository _scheduleRepository;

        public AdminDeleteScheduleUseCase(IDoctorScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task ExecuteAsync(int scheduleId)
        {
            await _scheduleRepository.DeleteScheduleAsync(scheduleId);
        }
    }
}