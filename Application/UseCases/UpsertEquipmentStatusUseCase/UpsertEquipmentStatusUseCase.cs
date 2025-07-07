using Application.Contracts;
using Application.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.UpsertEquipmentStatusUseCase
{
    public class UpsertEquipmentStatusUseCase(
        IEquipmentRepository equipmentRepository,
        ILogger<UpsertEquipmentStatusUseCase> logger
        ) : IUpsertEquipmentStatusUseCase
    {
        public async Task<Result> ExecuteAsync(Equipment equipment)
        {
            try
            {
                var upsertResult = await equipmentRepository.UpsertAsync(equipment);
                if (upsertResult.IsFailed)
                {
                    logger.LogError("[{Type}] Failed to upsert equipment. ErrorMessage: {ErrorMessage}",
                        GetType().Name,
                        upsertResult.ErrorMessage);
                    return Result.Failed(upsertResult.ErrorMessage!);
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "[{Type}] Got an exception during request. ErrorMessage: {ErrorMessage}",
                    GetType().Name,
                    ex.Message);

                return Result.Failed("An error occurred while updating equipment status.");
            }
        }
    }
}
