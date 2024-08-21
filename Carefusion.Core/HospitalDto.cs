using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core
{
    public class HospitalDto
    {
        [Key] [JsonIgnore]
        public int HospitalId { get; init; }
        [Required] [MaxLength(50)]
        public required string HospitalName { get; init; }
        [Required] [MaxLength(50)]
        public required string HospitalType { get; init; }
        [Required] [MaxLength(50)]
        public required string Province { get; init; }
        [Required] [MaxLength(50)]
        public required string District { get; init; }
        [Required] [MaxLength(255)]
        public required string FullAdress { get; init; }
    }
}
