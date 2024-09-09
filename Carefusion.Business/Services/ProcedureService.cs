using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class ProcedureService : IProcedureService
{
    private readonly IProcedureRepository _procedureRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public ProcedureService(IProcedureRepository procedureRepository, IMapper mapper)
    {
        _procedureRepository = procedureRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<ProcedureDto>> GetProcedures(int id)
    {
        var immunization = await _procedureRepository.GetAllAsync();
        var filteredProcedures = immunization.Where(a => a.PatientId == id);
        if (!filteredProcedures.Any())
            throw new InvalidOperationException("No immunizations found for the specified patient.");
        var procedureDtos = _mapper.Map<IEnumerable<ProcedureDto>>(filteredProcedures);
        return procedureDtos;
    }

    public async Task AddProcedureAsync(int id, ProcedureDto procedureDto)
    {
        var procedure = _mapper.Map<Procedure>(procedureDto);
        procedure.PatientId = id;
        procedure.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _procedureRepository.AddAsync(procedure);
    }

    public async Task UpdateProcedureAsync(int id, ProcedureDto procedureDto)
    {
        var procedure = await _procedureRepository.GetByIdAsync(id);
        if (procedure == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(procedureDto, procedure);
        procedure.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _procedureRepository.UpdateAsync(procedure);
    }

    public async Task<bool> DeleteProcedureAsync(int id)
    {
        var procedure = await _procedureRepository.GetByIdAsync(id);
        await _procedureRepository.DeleteAsync(procedure);
        return true;
    }
}