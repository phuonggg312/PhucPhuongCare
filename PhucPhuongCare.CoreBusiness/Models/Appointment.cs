using PhucPhuongCare.CoreBusiness.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhucPhuongCare.CoreBusiness.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public string PatientId { get; set; } = string.Empty;

        public int DoctorId { get; set; }

        public int TimeSlotId { get; set; }

        [StringLength(500)]
        public string? ReasonForVisit { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.PendingConfirmation;

        public string? DoctorNotes { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Doctor? Doctor { get; set; }
        public TimeSlot? TimeSlot { get; set; }
    }
}