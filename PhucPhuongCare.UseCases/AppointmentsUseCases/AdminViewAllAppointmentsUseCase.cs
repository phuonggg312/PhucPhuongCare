using PhucPhuongCare.UseCases.PluginInterfaces;
using PhucPhuongCare.UseCases.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class AdminViewAllAppointmentsUseCase : IAdminViewAllAppointmentsUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPatientProfileRepository _profileRepository; // << THÊM MỚI

        public AdminViewAllAppointmentsUseCase(
            IAppointmentRepository appointmentRepository,
            IUserRepository userRepository,
            IPatientProfileRepository profileRepository) // << THÊM MỚI
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository; // << THÊM MỚI
        }

        public async Task<IEnumerable<AppointmentViewModel>> ExecuteAsync()
        {
            var viewModels = new List<AppointmentViewModel>();
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();

            foreach (var appt in appointments)
            {
                var patientProfile = await _profileRepository.GetProfileByUserIdAsync(appt.PatientId);
                var patientFullName = $"{patientProfile?.LastName} {patientProfile?.FirstName}".Trim();

                // Chỉ gọi lấy email 1 lần và lưu vào biến
                var patientEmail = await _userRepository.GetPatientEmailByIdAsync(appt.PatientId);

                viewModels.Add(new AppointmentViewModel
                {
                    AppointmentId = appt.Id,
                    PatientId = appt.PatientId,
                    DoctorName = appt.Doctor?.FullName ?? "N/A",
                    PatientEmail = patientEmail,
                    // Nếu có họ tên thì dùng, không thì dùng email đã lấy
                    PatientFullName = !string.IsNullOrWhiteSpace(patientFullName) ? patientFullName : patientEmail,
                    AppointmentTime = appt.TimeSlot.SlotDateTime,
                    ReasonForVisit = appt.ReasonForVisit ?? "",
                    Status = appt.Status
                });
            }
            return viewModels;
        }
    }
}