using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required] [StringLength(255)]
        public required string FirstName { get; set; }
        [Required] [StringLength(50)]
        public required string LastName { get; set; }
        [Required]
        public required DateTime BirthDate { get; set; }
        [StringLength(5)]
        public string? Gender { get; set; }
        [StringLength(255)]
        public string? Email { get; set; }
        [StringLength(15)]
        public string? Telephone { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        [Required] [StringLength(3)]
        public required string BloodType { get; set; }
        [StringLength(50)]
        public string? Province { get; set; }
        [StringLength(1024)]
        public string? Picture { get; set; }
    }
}