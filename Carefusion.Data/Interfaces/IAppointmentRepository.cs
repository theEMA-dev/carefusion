using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> GetByIdAsync(int id);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
    Task<List<Appointment>> GetAllAsync();
    Task<MedicalHistoryDto> GetMedicalHistoryAsync(int id);
}