using PhucPhuongCare.CoreBusiness.Enums;
using PhucPhuongCare.CoreBusiness.Models;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class BookAppointmentUseCase : IBookAppointmentUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public BookAppointmentUseCase(
            IAppointmentRepository appointmentRepository,
            ITimeSlotRepository timeSlotRepository)
        {
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<bool> ExecuteAsync(int doctorId, int timeSlotId, string patientId, string reasonForVisit)
        {
            // Tạo một đối tượng Appointment mới
            var newAppointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                TimeSlotId = timeSlotId,
                ReasonForVisit = reasonForVisit,
                Status = AppointmentStatus.Confirmed, // Mặc định là đã xác nhận
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // 1. Thêm cuộc hẹn mới vào CSDL
            await _appointmentRepository.AddAppointmentAsync(newAppointment);

            // 2. Cập nhật trạng thái của khung giờ đã chọn thành "Đã đặt"
            await _timeSlotRepository.UpdateTimeSlotStatusAsync(timeSlotId, TimeSlotStatus.Booked);

            return true;
        }
    }
}