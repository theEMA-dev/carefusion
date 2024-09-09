using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Allergy
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int PatientId { get; set; }
    [Required]
    [StringLength(150)]
    public required string Name { get; init; }
    [StringLength(25)]
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
    public string? Manifestation { get; init; }
    [StringLength(25)]
    public string? ManifestationCode { get; init; }
    [Required]
    [StringLength(25)]
    public required string ClinicalStatus { get; init; }
    [Required]
    [StringLength(25)]
    public required string VerificationStatus { get; init; }
    public int? ReferrerId { get; init; }
    public int? PerformerId { get; init; }
    public string? Comment { get; init; }
    public int? AppointmentId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}