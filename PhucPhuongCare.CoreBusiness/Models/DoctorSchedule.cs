using System.ComponentModel.DataAnnotations;

namespace PhucPhuongCare.CoreBusiness.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        // ===== THAY ĐỔI Ở ĐÂY =====
        [Required]
        public TimeSpan StartTime { get; set; } // Đã đổi từ TimeOnly

        // ===== THAY ĐỔI Ở ĐÂY =====
        [Required]
        public TimeSpan EndTime { get; set; }   // Đã đổi từ TimeOnly

        [Required]
        public int SlotDurationMinutes { get; set; } = 30;

        public bool IsActive { get; set; } = true;

        // Navigation property
        public Doctor? Doctor { get; set; }
    }
}