using PhucPhuongCare.CoreBusiness.Models;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();
        Task<Specialty> GetSpecialtyByIdAsync(int specialtyId);
        Task AddSpecialtyAsync(Specialty specialty);
        Task UpdateSpecialtyAsync(Specialty specialty);
        Task DeleteSpecialtyAsync(int specialtyId);
    }
}