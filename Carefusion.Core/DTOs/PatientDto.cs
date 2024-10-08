﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class PatientDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [Required]
    [StringLength(150)]
    public required string Name { get; init; }
    [Required]
    public required DateOnly BirthDate { get; init; }
    [Required]
    [StringLength(25)]
    public required string Gender { get; init; }
    [Required]
    [StringLength(3)]
    public required string BloodType { get; init; }
    [Required]
    [StringLength(11)]
    public required string GovernmentId { get; init; }
    [DefaultValue(null)]
    [StringLength(1024)]
    public string? Picture { get; init; }
    [DefaultValue(null)]
    public int? AssignedPractitionerId { get; init; }
    [DefaultValue(null)]
    [JsonIgnore]
    public string? PractitionerName { get; set; }
    public string? AssignedPractitioner => PractitionerName;
    [DefaultValue(null)]
    [StringLength(50)]
    public string? HealthcareProvider { get; init; }
    [Required]
    [DefaultValue(true)]
    public required bool Active { get; init; }
    [Required]
    [DefaultValue(false)]
    public required bool Deceased { get; init; }
    public List<CommunicationDto>? Communication { get; init; }
    public int DebugPatientId => Identifier;
}