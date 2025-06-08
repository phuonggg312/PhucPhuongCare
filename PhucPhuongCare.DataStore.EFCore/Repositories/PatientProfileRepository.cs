using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class PatientProfileRepository : IPatientProfileRepository
    {
        private readonly AppDbContext _context;
        public PatientProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PatientProfile?> GetProfileByUserIdAsync(string userId)
        {
            return await _context.PatientProfiles.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
        }

        public async Task SaveProfileAsync(PatientProfile profile)
        {
            var existingProfile = await _context.PatientProfiles.FirstOrDefaultAsync(p => p.ApplicationUserId == profile.ApplicationUserId);
            if (existingProfile == null)
            {
                // Nếu chưa có thì tạo mới
                _context.PatientProfiles.Add(profile);
            }
            else
            {
                // Nếu đã có thì cập nhật
                existingProfile.FirstName = profile.FirstName;
                existingProfile.LastName = profile.LastName;
            }
            await _context.SaveChangesAsync();
        }
    }
}