using Application.Contracts;
using Application.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetEquipmentStatusUseCase
{
    public class GetEquipmentStatusUseCase(
        IEquipmentRepository equipmentRepository,
        ILogger<GetEquipmentStatusUseCase> logger
        ) : IGetEquipmentStatusUseCase
    {
        public async Task<Result<IEnumerable<Equipment>>> ExecuteAsync()
        {
            try
            {
                var equipmentsResult = await equipmentRepository.GetAllAsync();
                if (equipmentsResult.IsFailed)
                {
                    logger.LogError("[{Type}] Failed to fetch equipment status. ErrorMessage: {ErrorMessage}",
                        GetType().Name,
                        equipmentsResult.ErrorMessage);
                    return Result<IEnumerable<Equipment>>.Failed(equipmentsResult.ErrorMessage!);
                }
                return Result<IEnumerable<Equipment>>.Success(equipmentsResult.Value!);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "[{Type}] Got an exception during request. ErrorMessage: {ErrorMessage}",
                    GetType().Name,
                    ex.Message);

                return Result<IEnumerable<Equipment>>.Failed("An error occurred while fetching equipment status.");
            }
        }
    }
}
