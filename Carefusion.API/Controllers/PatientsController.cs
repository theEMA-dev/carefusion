using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientService.AddPatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatient), new { id = patientDto.PatientID }, patientDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto patientDto)
        {
            if (id != patientDto.PatientID)
            {
                return BadRequest("Patient ID mismatch.");
            }

            try
            {
                await _patientService.UpdatePatientAsync(id, patientDto);
                return NoContent();
            }
            catch (Utilities.NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the patient.");
            }
        }

    }
}