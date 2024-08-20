using System.Text.Json.Serialization;

namespace Carefusion.Core
{
    public class PatientDto
    {
        [JsonIgnore] public int PatientId { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateTime BirthDate { get; init; }
        public string? Gender { get; init; }
        public string? Email { get; init; }
        public required string Telephone { get; init; }
        public decimal? Height { get; init;}
        public decimal? Weight { get; init; }
        public required string BloodType { get; init; }
        public string? Province { get; init; }
        public string? Picture { get; init; }

    }
}