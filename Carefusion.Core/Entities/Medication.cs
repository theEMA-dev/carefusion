using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Medication
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int PatientId { get; set; }
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
    public string? Manufacturer { get; init; }
    [Required]
    [StringLength(50)]
    public required string Type { get; init; }
    [StringLength(50)]
    public string? Application { get; init; }
    [StringLength(1024)]
    public string? ProspectusLink { get; init; }
    [StringLength(255)]
    public string? Comment { get; init; }
    public int? PrescriberId { get; init; }
    public int? AppointmentId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}