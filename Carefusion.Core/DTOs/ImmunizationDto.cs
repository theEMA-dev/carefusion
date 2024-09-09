using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class ImmunizationDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [JsonIgnore]
    public int PatientId { get; set; }
    [Required]
    [StringLength(255)]
    public required string Name { get; init; }
    [Required]
    [StringLength(15)]
    public required string Code { get; init; }
    [Required]
    [StringLength(25)]
    public required string Status { get; init; }
    [StringLength(50)]
    [DefaultValue(null)]
    public string? Manufacturer { get; init; }
    [Required]
    [StringLength(50)]
    public required string Type { get; init; }
    [StringLength(25)]
    [DefaultValue(null)]
    public string? Application { get; init; }
    [Required]
    [DefaultValue(1)]
    public required int DoseNumber { get; init; }
    [Required]
    public required int DoseTotal { get; init; }
    [DefaultValue(null)]
    public int? ReferrerId { get; init; }
    [DefaultValue(null)]
    public int? PerformerId { get; init; }
    [StringLength(255)]
    [DefaultValue(null)]
    public string? VaccineDescriptor { get; init; }
    [DefaultValue(null)]
    public int? AppointmentId { get; init; }
}