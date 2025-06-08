using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly AppDbContext _context;

        public SpecialtyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSpecialtyAsync(Specialty specialty)
        {
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpecialtyAsync(int specialtyId)
        {
            var specialty = await _context.Specialties.FindAsync(specialtyId);
            if (specialty == null) return;

            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync()
        {
            return await _context.Specialties.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<Specialty> GetSpecialtyByIdAsync(int specialtyId)
        {
            return await _context.Specialties.FindAsync(specialtyId);
        }

        public async Task UpdateSpecialtyAsync(Specialty specialty)
        {
            _context.Entry(specialty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}