using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Appointment
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int PatientId { get; set; }
    [Required]
    [StringLength(50)]
    public required string Type { get; init; }
    [Required]
    [StringLength(25)]
    public required string Status { get; init; }
    [StringLength(255)]
    public string? PatientComment { get; init; }
    [Required]
    public required DateTime Time { get; set; }
    [Required]
    public required int HospitalId { get; init; }
    [Required]
    public required int DepartmentId { get; init; }
    public int? PractitionerId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}