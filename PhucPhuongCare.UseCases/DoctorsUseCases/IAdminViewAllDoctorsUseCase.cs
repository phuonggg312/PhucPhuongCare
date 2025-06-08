using PhucPhuongCare.CoreBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public interface IAdminViewAllDoctorsUseCase
    {
        Task<IEnumerable<Doctor>> ExecuteAsync();
    }
}