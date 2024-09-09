using Carefusion.Core.Entities;

namespace Carefusion.Core.DTOs;

public class MedicalHistoryDto
{
    public required Patient? Patient { get; set; }
    public List<Appointment>? Appointments { get; set; } = [];
    public List<Allergy>? Allergies { get; set; } = [];
    public List<LabTest>? LabTests { get; set; } = [];
    public List<Imaging>? Imaging { get; set; } = [];
    public List<Immunization>? Immunizations { get; set; } = [];
    public List<Medication>? Medications { get; set; } = [];
    public List<Procedure>? Procedures { get; set; } = [];
}