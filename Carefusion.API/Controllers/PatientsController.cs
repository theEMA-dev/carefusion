using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.DTOs;
using Carefusion.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CS0168 // Variable is declared but never used

namespace Carefusion.Web.Controllers;

/// <inheritdoc />
[Route("api/v1/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IAllergyService _allergyService;
    private readonly IImagingService _imagingService;
    private readonly ILabTestService _labTestService;
    private readonly IImmunizationService _immunizationService;
    private readonly IMedicationService _medicationService;
    private readonly IProcedureService _procedureService;
    private readonly IAppointmentService _appointmentService;

    /// <inheritdoc />
    public PatientsController(IPatientService patientService, IAllergyService allergyService, IImagingService imagingService, IImmunizationService immunizationService, ILabTestService labTestService, IMedicationService medicationService, IProcedureService procedureService, IAppointmentService appointmentService)
    {
        _patientService = patientService;
        _allergyService = allergyService;
        _imagingService = imagingService;
        _immunizationService = immunizationService;
        _labTestService = labTestService;
        _medicationService = medicationService;
        _procedureService = procedureService;
        _appointmentService = appointmentService;
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
    [HttpDelete("{id:int}")]
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

    /// <summary>
    /// Retrieve allergies
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/allergies")]
    public async Task<IActionResult> GetPatientAllergies(int id)
    {
        try
        {
            var allergies = await _allergyService.GetAllergies(id);
            return Ok(allergies);
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
    /// Modify allergies
    /// </summary>
    /// <param name="id"></param>
    /// <param name="allergyDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/allergies")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateAllergy(int id, [FromBody] AllergyDto allergyDto)
    {
            await _allergyService.UpdateAllergyAsync(id, allergyDto);
            return Ok();
    }
    /// <summary>
    /// Delete allergy by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/allergies")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> DeleteAllergy(int id)
    {
        try
        {
            var result = await _allergyService.DeleteAllergyAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new allergy record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="allergyDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/allergies")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddAllergy(int id, [FromBody] AllergyDto allergyDto)
    {
        try
        {
            await _allergyService.AddAllergyAsync(id, allergyDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieve imaging
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/imaging")]
    public async Task<IActionResult> GetPatientImaging(int id)
    {
        try
        {
            var imaging = await _imagingService.GetImaging(id);
            return Ok(imaging);
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
    /// Modify imaging
    /// </summary>
    /// <param name="id"></param>
    /// <param name="imagingDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/imaging")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateImaging(int id, [FromBody] ImagingDto imagingDto)
    {
        await _imagingService.UpdateImagingAsync(id, imagingDto);
        return Ok();
    }

    /// <summary>
    /// Delete imaging by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/imaging")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> DeleteImaging(int id)
    {
        try
        {
            var result = await _imagingService.DeleteImagingAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new imaging record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="imagingDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/imaging")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddImaging(int id, [FromBody] ImagingDto imagingDto)
    {
        try
        {
            await _imagingService.AddImagingAsync(id, imagingDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieve immunizations
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/immunizations")]
    public async Task<IActionResult> GetPatientImmunization(int id)
    {
        try
        {
            var immunization = await _immunizationService.GetImmunizations(id);
            return Ok(immunization);
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
    /// Modify immunizations
    /// </summary>
    /// <param name="id"></param>
    /// <param name="immunizationDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/immunizations")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateImmunization(int id, [FromBody] ImmunizationDto immunizationDto)
    {
        await _immunizationService.UpdateImmunizationAsync(id, immunizationDto);
        return Ok();
    }

    /// <summary>
    /// Delete immunization by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/immunizations")]
    public async Task<IActionResult> DeleteImmunization(int id)
    {
        try
        {
            var result = await _immunizationService.DeleteImmunizationAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new immunization record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="immunizationDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/immunizations")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddImmunization(int id, [FromBody] ImmunizationDto immunizationDto)
    {
        try
        {
            await _immunizationService.AddImmunizationAsync(id, immunizationDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieve lab tests
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/lab")]
    public async Task<IActionResult> GetPatientLabTest(int id)
    {
        try
        {
            var labTest = await _labTestService.GetLabTests(id);
            return Ok(labTest);
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
    /// Modify lab tests
    /// </summary>
    /// <param name="id"></param>
    /// <param name="labTestDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/lab")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateLabTest(int id, [FromBody] LabTestDto labTestDto)
    {
        await _labTestService.UpdateLabTestAsync(id, labTestDto);
        return Ok();
    }

    /// <summary>
    /// Delete lab test by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/lab")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> DeleteLabTest(int id)
    {
        try
        {
            var result = await _labTestService.DeleteLabTestAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new lab test record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="labTestDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/lab")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddLabTest(int id, [FromBody] LabTestDto labTestDto)
    {
        try
        {
            await _labTestService.AddLabTestAsync(id, labTestDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieve medications
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/medication")]
    public async Task<IActionResult> GetPatientMedication(int id)
    {
        try
        {
            var medication = await _medicationService.GetMedications(id);
            return Ok(medication);
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
    /// Modify medications
    /// </summary>
    /// <param name="id"></param>
    /// <param name="medicationDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/medication")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateMedication(int id, [FromBody] MedicationDto medicationDto)
    {
        await _medicationService.UpdateMedicationAsync(id, medicationDto);
        return Ok();
    }

    /// <summary>
    /// Delete a medication by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/medication")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> DeleteMedication(int id)
    {
        try
        {
            var result = await _medicationService.DeleteMedicationAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new medication record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="medicationDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/medication")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddMedication(int id, [FromBody] MedicationDto medicationDto)
    {
        try
        {
            await _medicationService.AddMedicationAsync(id, medicationDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieve medications
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/procedure")]
    public async Task<IActionResult> GetPatientProcedure(int id)
    {
        try
        {
            var procedure = await _procedureService.GetProcedures(id);
            return Ok(procedure);
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
    /// Modify procedures
    /// </summary>
    /// <param name="id"></param>
    /// <param name="procedureDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/procedure")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateProcedure(int id, [FromBody] ProcedureDto procedureDto)
    {
        await _procedureService.UpdateProcedureAsync(id, procedureDto);
        return Ok();
    }

    /// <summary>
    /// Delete a procedure
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/procedure")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> DeleteProcedure(int id)
    {
        try
        {
            var result = await _procedureService.DeleteProcedureAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new procedure record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="procedureDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/procedure")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddProcedure(int id, [FromBody] ProcedureDto procedureDto)
    {
        try
        {
            await _procedureService.AddProcedureAsync(id, procedureDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieve medications
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/appointment")]
    public async Task<IActionResult> GetPatientAppointment(int id)
    {
        try
        {
            var appointment = await _appointmentService.GetAppointments(id);
            return Ok(appointment);
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
    /// Modify procedures
    /// </summary>
    /// <param name="id"></param>
    /// <param name="appointmentDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/appointment")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto appointmentDto)
    {
        await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
        return Ok();
    }

    /// <summary>
    /// Delete a procedure
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/appointment")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        try
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            return !result ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// Create a new procedure record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="appointmentDto"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/appointment")]
    [Authorization.ApiKeyAuth]
    public async Task<IActionResult> AddAppointment(int id, [FromBody] AppointmentDto appointmentDto)
    {
        try
        {
            await _appointmentService.AddAppointmentAsync(id, appointmentDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get full medical history of patient
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/medical-history")]
    public async Task<IActionResult> GetPatientHistory(int id)
    {
        try
        {
            var appointment = await _appointmentService.GetMedicalHistoryAsync(id);
            return Ok(appointment);
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
}