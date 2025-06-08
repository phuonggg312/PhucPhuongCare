using System.ComponentModel.DataAnnotations;

namespace PhucPhuongCare.CoreBusiness.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } // Khóa ngoại liên kết tới bảng AspNetUsers

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }
    }
}