using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Procedure
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
    public required string Type { get; init; }
    [Required]
    [StringLength(25)]
    public required string Status { get; init; }
    [Required]
    [StringLength(150)]
    public required string BodySite { get; init; }
    [Required]
    [StringLength(50)]
    public required string Outcome { get; init; }
    [Required]
    public required bool Followup { get; init; }
    [StringLength(255)]
    public string? Comment { get; init; }
    [Required]
    public required int HospitalId { get; init; }
    [Required]
    public required int DepartmentId { get; init; }
    public int? ReferrerId { get; init; }
    public int? PerformerId { get; init; }
    public int? AppointmentId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}