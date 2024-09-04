﻿using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities;

public class Practitioner
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    [StringLength(150)]
    public required string Name { get; init; }
    [Required]
    public required DateOnly BirthDate { get; init; }
    [Required]
    [StringLength(25)]
    public required string Gender { get; init; }
    [StringLength(25)]
    public string? Specialty { get; init; }
    [StringLength(25)]
    public string? Title { get; init; }
    [StringLength(50)]
    public string? Role { get; init; }
    [Required]
    [StringLength(11)]
    public required string GovernmentId { get; init; }
    [StringLength(1024)]
    public string? Picture { get; init; }
    public int? AssignedHospital { get; init; }
    public int? AssignedDepartment { get; init; }
    [Required]
    public required bool Active { get; init; }
    public List<Communication>? Communication { get; set; } = [];
    [Required]
    public required DateTime RecordUpdated { get; set; }
}