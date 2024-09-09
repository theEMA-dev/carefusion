using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class LabTestService : ILabTestService
{
    private readonly ILabTestRepository _labTestRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public LabTestService(ILabTestRepository labTestRepository, IMapper mapper)
    {
        _labTestRepository = labTestRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<LabTestDto>> GetLabTests(int id)
    {
        var labTest = await _labTestRepository.GetAllAsync();
        var filteredLabTest = labTest.Where(a => a.PatientId == id);
        var labTests = filteredLabTest.ToList();
        if (labTests.Count == 0)
            throw new InvalidOperationException("No lab tests found for the specified patient.");
        var groupedLabTest = labTests.GroupBy(a => a.AppointmentId)
                                            .OrderByDescending(g => g.Key);
        var labTestDtos = groupedLabTest.SelectMany(g => _mapper.Map<IEnumerable<LabTestDto>>(g));
        return labTestDtos;
    }

    public async Task AddLabTestAsync(int id, LabTestDto labTestDto)
    {
        var labTest = _mapper.Map<LabTest>(labTestDto);
        labTest.PatientId = id;
        labTest.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _labTestRepository.AddAsync(labTest);
    }

    public async Task UpdateLabTestAsync(int id, LabTestDto labTestDto)
    {
        var labTest = await _labTestRepository.GetByIdAsync(id);
        if (labTest == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(labTestDto, labTest);
        labTest.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _labTestRepository.UpdateAsync(labTest);
    }

    public async Task<bool> DeleteLabTestAsync(int id)
    {
        var labTest = await _labTestRepository.GetByIdAsync(id);
        await _labTestRepository.DeleteAsync(labTest);
        return true;
    }
}