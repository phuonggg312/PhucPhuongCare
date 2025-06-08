using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public class ViewDoctorByIdUseCase : IViewDoctorByIdUseCase
    {
        private readonly IDoctorRepository _doctorRepository;

        public ViewDoctorByIdUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> ExecuteAsync(int doctorId)
        {
            // Chúng ta đã có sẵn phương thức này trong Repository
            return await _doctorRepository.GetDoctorByIdAsync(doctorId);
        }
    }
}