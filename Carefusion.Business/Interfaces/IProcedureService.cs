using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IProcedureService
{
    Task<IEnumerable<ProcedureDto>> GetProcedures(int id);
    Task AddProcedureAsync(int id, ProcedureDto procedureDto);
    Task UpdateProcedureAsync(int id, ProcedureDto procedureDto);
    Task<bool> DeleteProcedureAsync(int id);
}