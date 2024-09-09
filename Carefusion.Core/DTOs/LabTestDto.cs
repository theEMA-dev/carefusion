using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class LabTestDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [JsonIgnore]
    public int PatientId { get; init; }
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
    [DefaultValue(null)]
    public string? Reference { get; init; }
    [Required]
    [StringLength(25)]
    public required string Result { get; init; }
    [Required]
    [StringLength(15)]
    public required string Unit { get; init; }
    [Required]
    public required bool OutOfReference { get; init; }
    [DefaultValue(null)]
    public int? ReferrerId { get; init; }
    [DefaultValue(null)]
    public int? PerformerId { get; init; }
    [DefaultValue(null)]
    public int? AppointmentId { get; set; }
}