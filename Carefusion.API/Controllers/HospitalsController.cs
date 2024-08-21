using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;

namespace Carefusion.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        public HospitalsController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospital(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            return Ok(hospital);
        }

        [HttpPost]
        [Utilities.ApiKeyAuth]
        public async Task<IActionResult> AddHospital([FromBody] HospitalDto hospitalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _hospitalService.AddHospitalAsync(hospitalDto);
            return CreatedAtAction(nameof(GetHospital), new { id = hospitalDto.HospitalId }, hospitalDto);
        }

        [HttpPut("{id}")]
        [Utilities.ApiKeyAuth]
        public async Task<IActionResult> UpdateHospital(int id, [FromBody] HospitalDto hospitalDto)
        {
            try
            {
                await _hospitalService.UpdateHospitalAsync(id, hospitalDto);
                return NoContent();
            }
            catch (Utilities.NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the hospital.");
            }
        }
        [HttpDelete("{id}")]
        [Utilities.ApiKeyAuth]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            var result = await _hospitalService.DeleteHospitalAsync(id);
            if (!result)
            {
                return NotFound(); // Return 404 if the hospital is not found
            }

            return NoContent(); // Return 204 No Content on successful deletion
        }


    }
}