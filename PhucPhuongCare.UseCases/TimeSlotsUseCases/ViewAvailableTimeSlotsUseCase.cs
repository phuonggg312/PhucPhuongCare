using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.TimeSlotsUseCases
{
    public class ViewAvailableTimeSlotsUseCase : IViewAvailableTimeSlotsUseCase
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public ViewAvailableTimeSlotsUseCase(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<IEnumerable<TimeSlot>> ExecuteAsync(int doctorId, DateTime date)
        {
            return await _timeSlotRepository.GetAvailableTimeSlotsAsync(doctorId, date);
        }
    }
}