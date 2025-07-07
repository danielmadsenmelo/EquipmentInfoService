using Application.Models;

namespace Application.UseCases.UpsertEquipmentStatusUseCase
{
    public interface IUpsertEquipmentStatusUseCase
    {
        Task<Result> ExecuteAsync(Equipment equipment);
    }
}