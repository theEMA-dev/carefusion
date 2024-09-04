using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities
{
    public class Communication
    {
        [Key]
        public int Identifier { get; init; }
        [StringLength(50)]
        public string? Contact { get; init; }
        [StringLength(15)]
        public string? PhoneNumber { get; init; }
        [StringLength(100)]
        public string? Email { get; init; }
        [Required]
        [StringLength(50)]
        public required string Language { get; init; }
        [Required]
        public required bool NeedDisabilityAssistance { get; init; }
        [StringLength(255)]
        public string? Note { get; init; }
        public int? PatientIdentifier { get; init; }
        public int? PractitionerIdentifier { get; init; }
    }
}
