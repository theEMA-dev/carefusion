using System.ComponentModel;

namespace Carefusion.Core
{
    public enum HospitalSort
    {
        alphabeticalAsc,
        alphabeticalDesc,
        numberOfBedsAsc,
        numberOfBedsDesc,
        newest,
        oldest
    }
    public enum Gender
    {
        male,
        female,
        other,
        unknown
    }
    public enum BloodType
    {
        [Description("A+")]
        aPositive,
        [Description("A-")]
        aNegative,
        [Description("B+")]
        bPositive,
        [Description("B-")]
        bNegative,
        [Description("AB+")]
        abPositive,
        [Description("AB-")]
        abNegative,
        [Description("O+")]
        oPositive,
        [Description("O-")]
        oNegative
    }
    public enum PatientSort
    {
        alphabeticalAsc,
        alphabeticalDesc,
        newest,
        oldest
    }
}
