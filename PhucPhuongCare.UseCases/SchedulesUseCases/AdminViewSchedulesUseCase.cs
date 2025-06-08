using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SchedulesUseCases
{
    public class AdminViewSchedulesUseCase : IAdminViewSchedulesUseCase
    {
        private readonly IDoctorScheduleRepository _scheduleRepository;

        public AdminViewSchedulesUseCase(IDoctorScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<DoctorSchedule>> ExecuteAsync(int doctorId)
        {
            return await _scheduleRepository.GetSchedulesByDoctorAsync(doctorId);
        }
    }
}