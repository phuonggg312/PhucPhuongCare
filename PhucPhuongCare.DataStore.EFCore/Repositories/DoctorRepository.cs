using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            return await _context.Doctors
                                 .Include(d => d.Specialty) // Lấy cả thông tin Specialty
                                 .FirstOrDefaultAsync(d => d.Id == doctorId);
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(int specialtyId)
        {
            return await _context.Doctors
                                 .Where(d => d.SpecialtyId == specialtyId)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.Include(d => d.Specialty).ToListAsync();
        }
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            // Tìm bác sĩ trong DB bằng Id
            var docToUpdate = await _context.Doctors.FindAsync(doctor.Id);
            if (docToUpdate != null)
            {
                // Cập nhật các thuộc tính
                docToUpdate.FullName = doctor.FullName;
                docToUpdate.Degree = doctor.Degree;
                docToUpdate.Bio = doctor.Bio;
                docToUpdate.SpecialtyId = doctor.SpecialtyId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null) return;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}