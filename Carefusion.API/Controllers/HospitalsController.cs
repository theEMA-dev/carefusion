using Microsoft.AspNetCore.Mvc;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
#pragma warning disable CS0168 // Variable is declared but never used

namespace Carefusion.Web.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        /// <inheritdoc />
        public HospitalsController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        /// <summary>
        /// Finds a hospital by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok if hospital is found, not found if hospital cannot be found.</returns>
        /// <response code="200">Hospital is found</response>
        /// <response code="404">Hospital cannot be found</response>
        /// <response code ="500">Internal server error</response>
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
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Adds a new hospital
        /// </summary>
        /// <returns>Ok if new hospital is added, bad request if request is problematic.</returns>
        /// <response code="201">Hospital is added</response>
        /// <response code="401">Unauthorized, please enter API key</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Utilities.ApiKeyAuth]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddHospital([FromBody] HospitalDto hospitalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _hospitalService.AddHospitalAsync(hospitalDto);
                return CreatedAtAction(nameof(GetHospital), new { id = hospitalDto.HospitalId }, hospitalDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Modifies a hospital by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hospitalDto"></param>
        /// <returns>No content if successful, not found if the hospital does not exist.</returns>
        /// <response code="200">Changes applied</response>
        /// <response code="401">Unauthorized, please enter API key</response>
        /// <response code="404">Hospital does not exist</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Utilities.ApiKeyAuth]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHospital(int id, [FromBody] HospitalDto hospitalDto)
        {
            try
            {
                await _hospitalService.UpdateHospitalAsync(id, hospitalDto);
                return Ok();
            }
            catch (Utilities.NotFoundException)
            {
                return NotFound();
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
        /// <returns>No content if successful, not found if the hospital does not exist.</returns>
        /// <response code="204">Hospital deleted</response>
        /// <response code="401">Unauthorized, please enter API key</response>
        /// <response code="404">Hospital does not exist</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Utilities.ApiKeyAuth]
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


    }
}