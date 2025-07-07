using Application.Models;
using Application.UseCases.GetEquipmentStatusUseCase;
using Microsoft.AspNetCore.Mvc;


namespace EquipmentInfoApi.Controllers.V1.GetEquipmentStatus
{
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class GetEquipmentStatusController(
        IGetEquipmentStatusUseCase getEquipmentStatusUseCase,
        ILogger<GetEquipmentStatusController> logger
        ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Equipment>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEquipments()
        {
            try
            {
                var equipmentsResult = await getEquipmentStatusUseCase.ExecuteAsync();
                if (!equipmentsResult.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, equipmentsResult.ErrorMessage);
                }

                return Ok(equipmentsResult.Value!);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "[{Type}] Got an exception during request. ErrorMessage: {ErrorMessage}",
                    GetType().Name,
                    ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
