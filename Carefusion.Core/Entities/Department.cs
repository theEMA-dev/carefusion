using System.ComponentModel.DataAnnotations;

namespace Carefusion.Core.Entities;

public class Department
{
    [Key]
    public int Identifier { get; init; }
    [Required]
    public required int HospitalId { get; set; }
    [Required]
    [StringLength(25)]
    public required string Name { get; init; }
    [StringLength(50)]
    public string? DetailedName { get; init; }
    [Required]
    public int RegisteredPractitioners { get; set; }
    [Required]
    public required DateTime RecordUpdated { get; set; }
}
