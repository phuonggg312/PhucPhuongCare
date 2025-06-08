using System.ComponentModel.DataAnnotations;

namespace PhucPhuongCare.CoreBusiness.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        public int SpecialtyId { get; set; }

        [StringLength(1000)]
        public string? Bio { get; set; }

        [StringLength(150)]
        public string? Degree { get; set; }

        public string? ProfileImageUrl { get; set; }

        // Navigation property
        public Specialty? Specialty { get; set; }
    }
}