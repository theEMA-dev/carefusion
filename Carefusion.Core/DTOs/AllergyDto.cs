using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class AllergyDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [JsonIgnore]
    public int PatientId { get; init; }
    [Required]
    [StringLength(150)]
    public required string Name { get; init; }
    [StringLength(25)]
    [DefaultValue(null)]
    public string? Code { get; init; }
    [Required]
    [StringLength(25)]
    public required string Type { get; init; }
    [Required]
    [StringLength(25)]
    public required string Category { get; init; }
    [Required]
    [StringLength(15)]
    public required string Criticality { get; init; }
    [StringLength(150)]
    [DefaultValue(null)]
    public string? Manifestation { get; init; }
    [StringLength(25)]
    [DefaultValue(null)]
    public string? ManifestationCode { get; init; }
    [StringLength(25)]
    public required string ClinicalStatus { get; init; }
    [StringLength(25)]
    public required string VerificationStatus { get; init; }
    [DefaultValue(null)]
    public int? ReferrerId { get; init; }
    [DefaultValue(null)]
    public int? PerformerId { get; init; }
    [StringLength(255)]
    [DefaultValue(null)]
    public string? Comment { get; init; }
    [DefaultValue(null)]
    public int? AppointmentId { get; init; }
    public int DebugAllergyId => Identifier;
}