using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IMedicationService
{
    Task<IEnumerable<MedicationDto>> GetMedications(int id);
    Task AddMedicationAsync(int id, MedicationDto medicationDto);
    Task UpdateMedicationAsync(int id, MedicationDto medicationDto);
    Task<bool> DeleteMedicationAsync(int id);
}