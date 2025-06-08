using PhucPhuongCare.CoreBusiness.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.TimeSlotsUseCases
{
    public interface IViewAvailableTimeSlotsUseCase
    {
        Task<IEnumerable<TimeSlot>> ExecuteAsync(int doctorId, DateTime date);
    }
}