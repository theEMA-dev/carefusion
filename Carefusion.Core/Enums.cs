using System.ComponentModel;

namespace Carefusion.Core;

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
public enum BasicSort
{
    alphabeticalAsc,
    alphabeticalDesc,
    newest,
    oldest
}
public enum PractitionerTitle
{
    [Description("Prof. Dr.")]
    professorDr,
    [Description("Assoc. Prof. Dr.")]
    associateProfessorDr,
    [Description("Asst. Prof. Dr.")]
    assistantProfessorDr,
    [Description("Spec. Dr.")]
    specialistDr,
    [Description("Op. Dr.")]
    operatorDr,
    [Description("GP")]
    generalPractitioner,
    [Description("Res. Dr.")]
    residentDr,
    [Description("C. Phys.")]
    chiefPhysician,
    [Description("Dep. C.Phys.")]
    deputyChiefPhysician,
    [Description("Sr. Asst.")]
    seniorAssistant,
    [Description("Dr.")]
    dr
}
public enum PractitionerSpecialty
{
    neurology,
    specialty,
    radiology,
    cardiology,
    surgery,
    pathology,
    endocrinology,
    psychiatry,
    oncology,
    dermatology,
    orthopedics,
    pulmonology,
    nephrology,
    hematology,
    obstetrics,
    gynecology,
    urology,
    anesthesiology,
    rheumatology,
    allergy,
    geriatrics
}
