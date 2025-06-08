using PhucPhuongCare.CoreBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SpecialtiesUseCases
{
    public interface IViewSpecialtiesUseCase
    {
        Task<IEnumerable<Specialty>> ExecuteAsync();
    }
}