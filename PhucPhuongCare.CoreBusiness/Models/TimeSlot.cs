using PhucPhuongCare.CoreBusiness.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhucPhuongCare.CoreBusiness.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        [Required]
        public DateTime SlotDateTime { get; set; }

        [Required]
        public TimeSlotStatus Status { get; set; } = TimeSlotStatus.Available;

        // Navigation property
        public Doctor? Doctor { get; set; }
    }
}