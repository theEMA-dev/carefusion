using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class LabTest
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int PatientId { get; set; }
    [Required]
    [StringLength(150)]
    public required string Name { get; init; }
    [Required]
    [StringLength(25)]
    public required string Type { get; init; }
    [Required]
    [StringLength(25)]
    public required string Status { get; init; }
    [StringLength(50)]
    public string? Reference { get; init; }
    [Required]
    [StringLength(25)]
    public required string Result { get; init; }
    [Required]
    [StringLength(15)]
    public required string Unit { get; init; }
    [Required]
    public required bool OutOfReference { get; init; }
    public int? ReferrerId { get; init; }
    public int? PerformerId { get; init; }
    public int? AppointmentId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}