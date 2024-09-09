using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointments(int id)
    {
        var appointments = await _appointmentRepository.GetAllAsync();
        var filteredAppointments = appointments.Where(a => a.PatientId == id);
        if (!filteredAppointments.Any())
            throw new InvalidOperationException("No appointments found for the specified patient.");
        var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(filteredAppointments);
        return appointmentDtos;
    }   

    public async Task AddAppointmentAsync(int id, AppointmentDto appointmentDto)
    {
        var appointment = _mapper.Map<Appointment>(appointmentDto);
        appointment.PatientId = id;
        appointment.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _appointmentRepository.AddAsync(appointment);
    }

    public async Task UpdateAppointmentAsync(int id, AppointmentDto appointmentDto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(appointmentDto, appointment);
        appointment.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _appointmentRepository.UpdateAsync(appointment);
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        await _appointmentRepository.DeleteAsync(appointment);
        return true;
    }

    public async Task<IEnumerable<AppointmentDto>> GetActiveAppointments(int id)
    {
        var appointments = await _appointmentRepository.GetAllAsync();
        var filteredAppointments = appointments.Where(a => a.PatientId == id);
        var enumerable = filteredAppointments.ToList();
        if (enumerable.Count == 0)
            throw new InvalidOperationException("No appointments found for the specified date.");
        
        var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(
            enumerable.Where(a => a.Status == "scheduled"));
        return appointmentDtos;
    }

    public async Task<MedicalHistoryDto> GetMedicalHistoryAsync(int id)
    {
        return await _appointmentRepository.GetMedicalHistoryAsync(id);
    }
}