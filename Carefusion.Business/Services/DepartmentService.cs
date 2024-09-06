using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Data.Interfaces;
using Carefusion.Entities;

namespace Carefusion.Business.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<DepartmentDto>> GetDepartments(int id)
    {
        var departments = await _departmentRepository.GetAllAsync();
        var filteredDepartments = departments.Where(d => d.HospitalId == id);
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(filteredDepartments);
        return departmentDtos;
    }

    public async Task AddDepartmentAsync(int id, DepartmentDto departmentDto)
    {
        var department = _mapper.Map<Department>(departmentDto);
        department.HospitalId = id;
        department.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _departmentRepository.AddAsync(department);
    }

    public async Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(departmentDto, department);
        department.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _departmentRepository.UpdateAsync(department);
    }
    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var hospital = await _departmentRepository.GetByIdAsync(id);
        await _departmentRepository.DeleteAsync(hospital);
        return true;
    }

    public async Task<string?> GetDepartmentNameById(int? id)
    {
        if (id == null)
        {
            return null;
        }

        var department = await _departmentRepository.GetByIdAsync(id.Value);
        return department.Name;
    }
}