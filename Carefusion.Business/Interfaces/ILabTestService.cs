using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface ILabTestService
{
    Task<IEnumerable<LabTestDto>> GetLabTests(int id);
    Task AddLabTestAsync(int id, LabTestDto labTestDto);
    Task UpdateLabTestAsync(int id, LabTestDto labTestDto);
    Task<bool> DeleteLabTestAsync(int id);
}