using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class ImagingDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [JsonIgnore]
    public int PatientId { get; init; }
    [Required]
    [StringLength(15)]
    public required string Method { get; init; }
    [Required]
    [StringLength(25)]
    public required string Reason { get; init; }
    [Required]
    [StringLength(25)]
    public required string Status { get; init; }
    [Required]
    [StringLength(150)]
    public required string BodySite { get; init; }
    [DefaultValue(null)]
    public int? ReferrerId { get; init; }
    [DefaultValue(null)]
    public int? PerformerId { get; init; }
    [Required]
    public required int HospitalId { get; init; }
    [StringLength(255)]
    [DefaultValue(null)]
    public string? Comment { get; init; }
    [StringLength(1024)]
    [DefaultValue(null)]
    public string? Result { get; init; }
    [DefaultValue(null)]
    public int? AppointmentId { get; init; }
}