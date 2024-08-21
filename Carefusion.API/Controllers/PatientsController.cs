using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;

namespace Carefusion.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            return Ok(patient);
        }

        [HttpPost]
        [Utilities.ApiKeyAuth]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientService.AddPatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatient), new { id = patientDto.PatientId }, patientDto);
        }

        [HttpPut("{id}")]
        [Utilities.ApiKeyAuth]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto patientDto)
        {
            try
            {
                await _patientService.UpdatePatientAsync(id, patientDto);
                return Ok("Patient updated");
            }
            catch (Utilities.NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the patient.");
            }
        }
        [HttpDelete("{id}")]
        [Utilities.ApiKeyAuth]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (!result)
            {
                return NotFound(); // Return 404 if the patient is not found
            }

            return NoContent(); // Return 204 No Content on successful deletion
        }

    }
}