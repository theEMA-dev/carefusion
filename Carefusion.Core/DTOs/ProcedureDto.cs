using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class ProcedureDto
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
    [DefaultValue(false)]
    public required bool Followup { get; init; }
    [StringLength(255)]
    public string? Comment { get; init; }
    [Required]
    public required int HospitalId { get; init; }
    [Required]
    public required int DepartmentId { get; init; }
    [DefaultValue(null)]
    public int? ReferrerId { get; init; }
    [DefaultValue(null)]
    public int? PerformerId { get; init; }
    [DefaultValue(null)]
    public int? AppointmentId { get; init; }
}