using PhucPhuongCare.CoreBusiness.Models;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.DoctorsUseCases
{
    public interface IViewDoctorByIdUseCase
    {
        Task<Doctor> ExecuteAsync(int doctorId);
    }
}