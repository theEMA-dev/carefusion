using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core
{
    public class PatientDto
    { 
        [Key] [JsonIgnore]
        public int PatientId { get; init; }
        [Required] [MaxLength(255)]
        public required string FirstName { get; init; }
        [Required] [MaxLength(50)]
        public required string LastName { get; init; }
        [Required]
        public required DateTime BirthDate { get; init; }
        [Required] [MaxLength(3)]
        public required string BloodType { get; init; }
        public string FormattedBirthDate => BirthDate.ToString("yyyy-MM-dd");
        public string? Gender { get; init; }
        [MaxLength(255)]
        public string? Email { get; init; }
        [MaxLength(15)]
        public string? Telephone { get; init; }
        public decimal? Height { get; init;}
        public decimal? Weight { get; init; }
        [MaxLength(50)]
        public string? Province { get; init; }
        [MaxLength(1024)]
        public string? Picture { get; init; }

    }
}