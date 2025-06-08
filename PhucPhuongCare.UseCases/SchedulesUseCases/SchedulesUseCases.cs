using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SchedulesUseCases
{
    public class AdminAddScheduleUseCase : IAdminAddScheduleUseCase
    {
        private readonly IDoctorScheduleRepository _scheduleRepository;

        public AdminAddScheduleUseCase(IDoctorScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task ExecuteAsync(DoctorSchedule schedule)
        {
            await _scheduleRepository.AddScheduleAsync(schedule);
        }
    }
}