using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.Utilities;
#pragma warning disable CS0168 // Variable is declared but never used

namespace Carefusion.Web.Controllers;

/// <inheritdoc />
[Route("api/v1/[controller]")]
[ApiController]
public class HospitalsController : ControllerBase
{
    private readonly IHospitalService _hospitalService;
    private readonly IDepartmentService _departmentService;

    /// <inheritdoc />
    public HospitalsController(IHospitalService hospitalService, IDepartmentService departmentService)
    {
        _hospitalService = hospitalService;
        _departmentService = departmentService;
    }

    /// <summary>
    /// Finds a hospital by ID
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHospital(int id)
    {
        try
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            return Ok(hospital);
        }
        catch (InvalidOperationException)
        {
            return NotFound(new {});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Search hospitals
    /// </summary>
    /// <param name="q">Search</param>
    /// <param name="type">general | specialty | clinic | urgentCare | trainingAndResearch</param>
    /// <param name="sort"></param>
    /// <param name="emergencyServices"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="showInactive"></param>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchHospitals(
        [FromQuery] string? q,
        [FromQuery] string[]? type,
        [FromQuery] HospitalSort? sort,
        [FromQuery] bool? emergencyServices = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] bool showInactive = false)
    {
        try
        {
            var (hospitals, totalCount) = await _hospitalService.SearchHospitalsAsync(q ?? " ", type, sort, emergencyServices, pageNumber, pageSize, showInactive);
            if (totalCount == 0) return NotFound();

            return Ok(new { Hospitals = hospitals, TotalCount = totalCount });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    /// <summary>
    /// Adds a new hospital
    /// </summary>
    [HttpPost]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddHospital([FromBody] HospitalDto hospitalDto)
    {
        try
        {
            await _hospitalService.AddHospitalAsync(hospitalDto);
            return CreatedAtAction(nameof(GetHospital), new { id = hospitalDto.Identifier }, hospitalDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Modifies a hospital by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="hospitalDto"></param>
    [HttpPut("{id:int}")]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateHospital(int id, [FromBody] HospitalDto hospitalDto)
    {
        try
        {
            await _hospitalService.UpdateHospitalAsync(id, hospitalDto);
            return Ok();
        }
        catch (InvalidOperationException)
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the hospital.");
        }
    }
    /// <summary>
    /// Deletes a hospital by ID
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:int}")]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteHospital(int id)
    {
        try
        {
            var result = await _hospitalService.DeleteHospitalAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Adds a department to a hospital
    /// </summary>
    /// <param name="id">Enter Hospital ID:</param>
    /// <param name="departmentDto"></param>
    /// <returns></returns>
    [HttpPost("department/{id:int}")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddDepartment(int id, [FromBody] DepartmentDto departmentDto)
    {
        try
        {
            await _departmentService.AddDepartmentAsync(id, departmentDto);
            return CreatedAtAction(nameof(GetHospital), new { id = departmentDto.Identifier }, departmentDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Updates a department by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="departmentDto"></param>
    /// <returns></returns>
    [HttpPut("department/{id:int}")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
    {
        try
        {
            await _departmentService.UpdateDepartmentAsync(id, departmentDto);
            return Ok();
        }
        catch (InvalidOperationException)
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the department.");
        }
    }

    /// <summary>
    /// Deletes a department by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("department/{id:int}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        try
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}