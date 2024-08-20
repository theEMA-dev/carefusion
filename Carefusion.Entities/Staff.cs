using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Carefusion.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int DepartmentId { get; set; }
        public string ContactInfo { get; set; }
        public string EmploymentDetails { get; set; }
    }
}
