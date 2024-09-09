using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Immunization
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int PatientId { get; set; }
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
    public string? Manufacturer { get; init; }
    [Required]
    [StringLength(50)]
    public required string Type { get; init; }
    [StringLength(25)]
    public string? Application { get; init; }
    [Required]
    public required int DoseNumber { get; init; }
    [Required]
    public required int DoseTotal { get; init; }
    public int? ReferrerId { get; init; }
    public int? PerformerId { get; init; }
    [StringLength(255)]
    public string? VaccineDescriptor { get; init; }
    public int? AppointmentId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}