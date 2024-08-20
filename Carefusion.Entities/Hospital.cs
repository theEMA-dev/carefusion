using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities
{
    public class Hospital
    {
        [Required] public int HospitalId { get; init; }
        [StringLength(50)] public string HospitalName { get; set; } = null!;
        [StringLength(50)] public string HospitalType { get; set; } = null!;
        [StringLength(50)] public string Province { get; set; } = null!;
        [StringLength(50)] public string District { get; set; } = null!;
        [StringLength(255)] public string FullAdress { get; set; } = null!;

    }
}
