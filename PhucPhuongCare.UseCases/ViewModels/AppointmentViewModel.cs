using PhucPhuongCare.CoreBusiness.Enums;
using System;

namespace PhucPhuongCare.UseCases.ViewModels
{
    // Đảm bảo đây là 'public class'
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty; // << THÊM DÒNG NÀY
        public DateTime AppointmentTime { get; set; }
        public string ReasonForVisit { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }
    }
}