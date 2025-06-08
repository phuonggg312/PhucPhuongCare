using PhucPhuongCare.CoreBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public interface IViewDoctorsBySpecialtyUseCase
    {
        Task<IEnumerable<Doctor>> ExecuteAsync(int specialtyId);
    }
}