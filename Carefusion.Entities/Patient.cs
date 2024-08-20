using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities
{
    public class Patient
    {
        public int PatientId { get; init; }
        [StringLength(255)] public string FirstName { get; set; } = null!;
        [StringLength(50)] public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        [StringLength(5)] public string Gender { get; set; } = null!;
        [StringLength(100)] public string? Email { get; set; }
        [StringLength(15)] public string Telephone { get; set; } = null!;
        public Decimal? Height { get; set; }
        public Decimal? Weight { get; set; }
        [StringLength(3)] public string BloodType { get; set; } = null!;
        [StringLength(50)] public string Province { get; set; } = null!;
        [StringLength(1024)] public string? Picture { get; set; }
    }
}