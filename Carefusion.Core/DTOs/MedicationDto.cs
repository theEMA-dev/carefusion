using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class MedicationDto
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
    [StringLength(50)]
    public required string Barcode { get; init; }
    [Required]
    [StringLength(25)]
    public required string Status { get; init; }
    [StringLength(50)]
    [DefaultValue(null)]
    public string? Manufacturer { get; init; }
    [Required]
    [StringLength(50)]
    public required string Type { get; init; }
    [StringLength(50)]
    [DefaultValue(null)]
    public string? Application { get; init; }
    [StringLength(1024)]
    [DefaultValue(null)]
    public string? ProspectusLink { get; init; }
    [StringLength(255)]
    [DefaultValue(null)]
    public string? Comment { get; init; }
    [DefaultValue(null)]
    public int? PrescriberId { get; init; }
    [DefaultValue(null)]
    public int? AppointmentId { get; init; }
}