// ReSharper disable InconsistentNaming
namespace Carefusion.Core.Criterias
{
    public class HospitalFilterCriteria
    {
        public List<string>? type { get; set; }
        public bool showInactive { get; set; }
    }

    public class HospitalSortCriteria
    {
        public bool sortByNumberOfBeds { get; set; }
        public bool sortByEmergencyServices { get; set; }
        public bool sortByCity { get; set; }
        public bool sortByDistrict { get; set; }
    }
}
