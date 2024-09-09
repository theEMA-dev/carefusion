using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class DepartmentDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [JsonIgnore]
    [Required]
    public int HospitalId { get; init; }
    [Required]
    [StringLength(25)]
    public required string Name { get; init; }
    [StringLength(50)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DetailedName { get; init; }
    [JsonIgnore]
    public int RegisteredPractitioners { get; init; }

    public int PractitionerCount => RegisteredPractitioners;
    public int DebugDepartmentId => Identifier;
}