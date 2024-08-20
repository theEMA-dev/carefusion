using System.Text.Json.Serialization;

namespace Carefusion.Core
{
    public class HospitalDto
    {
        [JsonIgnore] public int HospitalId { get; init; }
        public required string HospitalName { get; init; }
        public required string HospitalType { get; init; }
        public required string Province { get; init; }
        public required string District { get; init; }
        public required string FullAdress { get; init; }
    }
}
