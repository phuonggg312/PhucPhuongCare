﻿using PhucPhuongCare.CoreBusiness.Enums;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.AppointmentsUseCases
{
    public class CancelAppointmentUseCase : ICancelAppointmentUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public CancelAppointmentUseCase(
            IAppointmentRepository appointmentRepository,
            ITimeSlotRepository timeSlotRepository) // Inject thêm
        {
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task ExecuteAsync(int appointmentId)
        {
            // Lấy thông tin cuộc hẹn để biết TimeSlotId là gì
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null) return;

            // 1. Cập nhật trạng thái cuộc hẹn thành CanceledByPatient
            await _appointmentRepository.UpdateAppointmentStatusAsync(appointmentId, AppointmentStatus.CanceledByPatient);

            // 2. Cập nhật trạng thái của TimeSlot thành Available
            if (appointment.TimeSlotId > 0)
            {
                await _timeSlotRepository.UpdateTimeSlotStatusAsync(appointment.TimeSlotId, TimeSlotStatus.Available);
            }
        }
    }
}