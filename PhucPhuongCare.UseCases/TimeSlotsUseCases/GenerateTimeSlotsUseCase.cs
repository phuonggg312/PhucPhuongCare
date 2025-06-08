using PhucPhuongCare.UseCases.PluginInterfaces;
using System;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.TimeSlotsUseCases
{
    public class GenerateTimeSlotsUseCase : IGenerateTimeSlotsUseCase
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public GenerateTimeSlotsUseCase(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task ExecuteAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            await _timeSlotRepository.GenerateSlotsForDoctorAsync(doctorId, startDate, endDate);
        }
    }
}