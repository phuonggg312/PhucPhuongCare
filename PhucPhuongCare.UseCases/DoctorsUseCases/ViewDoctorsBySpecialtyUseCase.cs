using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public class ViewDoctorsBySpecialtyUseCase : IViewDoctorsBySpecialtyUseCase
    {
        private readonly IDoctorRepository _doctorRepository;

        public ViewDoctorsBySpecialtyUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Doctor>> ExecuteAsync(int specialtyId)
        {
            return await _doctorRepository.GetDoctorsBySpecialtyAsync(specialtyId);
        }
    }
}