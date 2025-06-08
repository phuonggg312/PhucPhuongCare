using System.ComponentModel.DataAnnotations;

namespace PhucPhuongCare.CoreBusiness.Models
{
    public class Specialty
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}