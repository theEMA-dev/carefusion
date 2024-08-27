﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Carefusion.Core
{
    public class HospitalDto
    {
        [Key]
        [JsonIgnore]
        public int Identifier { get; init; }
        [Required]
        [StringLength(150)]
        public required string Name { get; init; }
        [Required]
        [StringLength(25)]
        public required string Code { get; init; }
        [AllowNull]
        [StringLength(25)]
        public string Affiliation { get; init; }
        [Required]
        [StringLength(25)]
        public required string Type { get; init; }
        [Required]
        public required int NumberOfBeds { get; init; }
        [Required]
        public required bool EmergencyServices { get; init; }
        [Required]
        [StringLength(25)]
        public required string City { get; init; }
        [Required]
        [StringLength(50)]
        public required string District { get; init; }
        [Required]
        [StringLength(25)]
        public required string ZipCode { get; init; }
        [Required]
        [StringLength(255)]
        public required string Address { get; init; }
        [AllowNull]
        [StringLength(25)]
        public string PhoneNumber { get; init; }
        [AllowNull]
        [StringLength(25)]
        public string FaxNumber { get; init; }
        [Required]
        public required bool Active { get; init; }
    }
}
