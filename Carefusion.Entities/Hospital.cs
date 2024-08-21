using System.ComponentModel.DataAnnotations;

namespace Carefusion.Entities
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }
        [Required] [StringLength(50)]
        public required string HospitalName { get; set; }
        [Required] [StringLength(50)]
        public required string HospitalType { get; set; }
        [Required] [StringLength(50)]
        public required string Province { get; set; }
        [Required] [StringLength(50)]
        public required string District { get; set; }
        [Required] [StringLength(255)]
        public required string FullAdress { get; set; }

    }
}
