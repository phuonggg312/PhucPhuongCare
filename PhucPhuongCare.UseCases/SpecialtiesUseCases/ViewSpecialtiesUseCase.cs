using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.SpecialtiesUseCases
{
    public class ViewSpecialtiesUseCase : IViewSpecialtiesUseCase
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public ViewSpecialtiesUseCase(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IEnumerable<Specialty>> ExecuteAsync()
        {
            return await _specialtyRepository.GetAllSpecialtiesAsync();
        }
    }
}