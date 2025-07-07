using Application.Models;

namespace Application.UseCases.GetEquipmentStatusUseCase
{
    public interface IGetEquipmentStatusUseCase
    {
        Task<Result<IEnumerable<Equipment>>> ExecuteAsync();
    }
}