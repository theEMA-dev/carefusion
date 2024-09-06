using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.Utilities;
#pragma warning disable CS0168 // Variable is declared but never used

namespace Carefusion.Web.Controllers;

/// <inheritdoc />
[Route("api/v1/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    /// <inheritdoc />
    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Finds a patient by ID
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPatient(int id)
    {
        try
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            return Ok(patient);
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
    /// Finds a patient by government ID
    /// </summary>
    /// <param name="govId"></param>
    [HttpGet("gov/{govId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPatientByGovId(string govId)
    {
        try
        {
            var patient = await _patientService.GetPatientByGovId(govId);
            return Ok(patient);
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
    /// Search patients
    /// </summary>
    /// <param name="q">Search</param>
    /// <param name="sort"></param>
    /// <param name="bloodType"></param>
    /// <param name="birthStartDate"></param>
    /// <param name="birthEndDate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="showDeceased"></param>
    /// <param name="showInactive"></param>
    /// <param name="gender"></param>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchPatients(
        [FromQuery] string? q,
        [FromQuery] Gender? gender,
        [FromQuery] BloodType? bloodType,
        [FromQuery] DateOnly? birthStartDate,
        [FromQuery] DateOnly? birthEndDate,
        [FromQuery] BasicSort? sort,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] bool showDeceased = false,
        [FromQuery] bool showInactive = false)
    {
        try
        {
            var (patients, totalCount) = await _patientService.SearchPatientsAsync(q, sort, gender, bloodType, birthStartDate, birthEndDate, pageNumber, pageSize, showDeceased, showInactive);
            if (totalCount == 0) return NotFound();

            return Ok(new { Patients = patients, TotalCount = totalCount });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Adds a new patient
    /// </summary>
    [HttpPost]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddPatient([FromBody] PatientDto patientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _patientService.AddPatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatient), new { id = patientDto.Identifier }, patientDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Modifies the patient by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patientDto"></param>
    [HttpPut("{id}")]
    [Authorization.ApiKeyAuth]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto patientDto)
    {
        try
        {
            await _patientService.UpdatePatientAsync(id, patientDto);
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
    /// Deletes a patient by ID
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
            var result = await _patientService.DeletePatientAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}