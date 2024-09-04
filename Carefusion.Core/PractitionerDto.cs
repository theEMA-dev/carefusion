using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core;

public class PractitionerDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [Required]
    [StringLength(150)]
    public required string Name { get; init; }
    [Required]
    public required DateOnly BirthDate { get; init; }
    [Required]
    [StringLength(25)]
    public required string Gender { get; init; }
    [StringLength(25)]
    [DefaultValue(null)]
    public string? Specialty { get; init; }
    [StringLength(25)]
    [DefaultValue(null)]
    public string? Title { get; init; }
    [StringLength(50)]
    [DefaultValue(null)]
    public string? Role { get; init; }
    [Required]
    [StringLength(11)]
    public required string GovernmentId { get; init; }
    [StringLength(1024)]
    [DefaultValue(null)]
    public string? Picture { get; init; }
    [DefaultValue(null)]
    public int? AssignedHospital { get; init; }
    [DefaultValue(null)]
    public int? AssignedDepartment { get; init; }
    [Required]
    [DefaultValue(true)]
    public required bool Active { get; init; }
    public List<CommunicationDto>? Communication { get; init; } = [];
    public int Id => Identifier;
}