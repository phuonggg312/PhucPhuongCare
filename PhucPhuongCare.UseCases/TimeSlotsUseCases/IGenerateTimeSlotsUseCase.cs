using System;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.TimeSlotsUseCases
{
    public interface IGenerateTimeSlotsUseCase
    {
        Task ExecuteAsync(int doctorId, DateTime startDate, DateTime endDate);
    }
}