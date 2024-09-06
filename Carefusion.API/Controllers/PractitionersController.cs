using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.Utilities;
#pragma warning disable CS0168 // Variable is declared but never used

namespace Carefusion.Web.Controllers;

/// <inheritdoc />
[Route("api/v1/[controller]")]
[ApiController]
public class PractitionersController : ControllerBase
{
    private readonly IPractitionerService _practitionerService;
    private readonly IHospitalService _hospitalService;
    private readonly IDepartmentService _departmentService;

    /// <inheritdoc />
    public PractitionersController(IPractitionerService practitionerService, IHospitalService hospitalService, IDepartmentService departmentService)
    {
        _practitionerService = practitionerService;
        _hospitalService = hospitalService;
        _departmentService = departmentService;
    }

    /// <summary>
    /// Finds a practitioner by ID
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPractitioner(int id)
    {
        try
        {
            var practitioner = await _practitionerService.GetPractitionerByIdAsync(id);
            var assignedHospital = await _hospitalService.GetHospitalNameById(practitioner.AssignedHospitalId) ?? null;
            var assignedDepartment =
                await _departmentService.GetDepartmentNameById(practitioner.AssignedDepartmentId) ?? null;
            practitioner.HospitalName = assignedHospital;
            practitioner.DepartmentName = assignedDepartment;
            return Ok(practitioner);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Finds a practitioner by government ID
    /// </summary>
    /// <param name="govId"></param>
    [HttpGet("gov/{govId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPractitionerByGovId(string govId)
    {
        try
        {
            var practitioner = await _practitionerService.GetPractitionerByGovId(govId);
            var assignedHospital = await _hospitalService.GetHospitalNameById(practitioner.AssignedHospitalId) ?? null;
            var assignedDepartment =
                await _departmentService.GetDepartmentNameById(practitioner.AssignedDepartmentId) ?? null;
            practitioner.HospitalName = assignedHospital;
            practitioner.DepartmentName = assignedDepartment;
            return Ok(practitioner);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Search practitioners
    /// </summary>
    /// <param name="q">Search</param>
    /// <param name="sort"></param>
    /// <param name="title"></param>
    /// <param name="specialty"></param>
    /// <param name="birthStartDate"></param>
    /// <param name="birthEndDate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="showInactive"></param>
    /// <param name="gender"></param>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchPractitioners(
        [FromQuery] string? q,
        [FromQuery] Gender? gender,
        [FromQuery] PractitionerTitle? title,
        [FromQuery] PractitionerSpecialty? specialty,
        [FromQuery] DateOnly? birthStartDate,
        [FromQuery] DateOnly? birthEndDate,
        [FromQuery] BasicSort? sort,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] bool showInactive = false)
    {
        try
        {
            var (practitioners, totalCount) = await _practitionerService.SearchPractitionerAsync(q ?? " ", sort, gender, title, specialty, birthStartDate, birthEndDate, pageNumber, pageSize, showInactive);
            if (totalCount == 0) return NotFound();

            var practitionerDtos = practitioners.ToList();
            foreach (var practitioner in practitionerDtos)
            {
                var assignedHospital = await _hospitalService.GetHospitalNameById(practitioner.AssignedHospitalId) ?? null;
                var assignedDepartment =
                    await _departmentService.GetDepartmentNameById(practitioner.AssignedDepartmentId) ?? null;
                practitioner.HospitalName = assignedHospital;
                practitioner.DepartmentName = assignedDepartment;
            }

            return Ok(new { Practitioners = practitionerDtos, TotalCount = totalCount });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Adds a new practitioner
    /// </summary>
    [HttpPost]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddPractitioner([FromBody] PractitionerDto practitionerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _practitionerService.AddPractitionerAsync(practitionerDto);
            return CreatedAtAction(nameof(GetPractitioner), new { id = practitionerDto.Identifier }, practitionerDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Modifies the practitioner by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="practitionerDto"></param>
    [HttpPut("{id}")]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePractitioner(int id, [FromBody] PractitionerDto practitionerDto)
    {
        try
        {
            await _practitionerService.UpdatePractitionerAsync(id, practitionerDto);
            return Ok();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Deletes a practitioner by ID
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePatient(int id)
    {
        try
        {
            var result = await _practitionerService.DeletePractitionerAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}