using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities
{
    public class Patient
    {
        [Key]
        public int Identifier { get; init; }
        [Required]
        [StringLength(150)]
        public required string Name { get; init; }
        [Required]
        public required DateOnly BirthDate { get; init; }
        [Required]
        [StringLength(25)]
        public required string Gender { get; init; }
        [Required]
        [StringLength(3)]
        public required string BloodType { get; init; }
        [Required]
        [StringLength(11)]
        public required string GovernmentId { get; init; }
        [StringLength(1024)]
        public string? Picture { get; init; }
        public int? AssignedPractitioner { get; init; }
        [StringLength(50)]
        public string? HealthcareProvider { get; init; }
        [StringLength(50)]
        public string? PreferredLanguage { get; init; }
        [Required]
        public required bool Active { get; init; }
        [Required]
        public required bool Deceased { get; init; }
        [Required]
        public required DateTime RecordUpdated { get; set; }
    }
}