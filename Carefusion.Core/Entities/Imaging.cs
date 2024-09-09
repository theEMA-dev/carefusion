using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Imaging
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int PatientId { get; set; }
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
    public int? ReferrerId { get; init; }
    public int? PerformerId { get; init; }
    [Required]
    public required int HospitalId { get; init; }
    [StringLength(255)]
    public string? Comment { get; init; }
    [StringLength(1024)]
    public string? Result { get; init; }
    public int? AppointmentId { get; init; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}