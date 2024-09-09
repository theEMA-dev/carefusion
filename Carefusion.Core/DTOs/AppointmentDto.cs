using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class AppointmentDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [JsonIgnore]
    public int PatientId { get; set; }
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
}