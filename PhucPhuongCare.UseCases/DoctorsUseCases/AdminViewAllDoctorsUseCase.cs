using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public class AdminViewAllDoctorsUseCase : IAdminViewAllDoctorsUseCase
    {
        private readonly IDoctorRepository _doctorRepository;

        public AdminViewAllDoctorsUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Doctor>> ExecuteAsync()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }
    }
}