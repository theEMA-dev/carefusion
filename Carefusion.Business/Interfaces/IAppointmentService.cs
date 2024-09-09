using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAppointments(int id);
    Task AddAppointmentAsync(int id, AppointmentDto appointmentDto);
    Task UpdateAppointmentAsync(int id, AppointmentDto appointmentDto);
    Task<bool> DeleteAppointmentAsync(int id);
    Task<MedicalHistoryDto> GetMedicalHistoryAsync(int id);
}