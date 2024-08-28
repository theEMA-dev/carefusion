using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.Utilities;
#pragma warning disable CS0168 // Variable is declared but never used

namespace Carefusion.Web.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
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
        /// <returns>Ok if patient is found, not found if patient cannot be found.</returns>
        /// <response code="200">Patient is found</response>
        /// <response code="404">Patient cannot be found</response>
        /// <response code ="500">Internal server error</response>
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
        /// Adds a new patient
        /// </summary>
        /// <returns>Ok if new patient is added, bad request if request is problematic.</returns>
        /// <response code="201">Patient added</response>
        /// <response code="401">Unauthorized, please enter API key</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
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
                return CreatedAtAction(nameof(GetPatient), new { id = patientDto.PatientId }, patientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Modifies the patient by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patientDto"></param>
        /// <returns>Ok if the changes are successful, not found if the patient does not exist.</returns>
        /// <response code="200">Changes applied</response>
        /// <response code="401">Unauthorized, please enter API key</response>
        /// <response code="404">Patient does not exist</response>
        /// <response code="500">Internal server error</response>
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
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>No content if successful, not found if the patient does not exist.</returns>
        /// <response code="204">Patient deleted</response>
        /// <response code="401">Unauthorized, please enter API key</response>
        /// <response code="404">Patient does not exist</response>
        /// <response code="500">Internal server error</response>
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
}