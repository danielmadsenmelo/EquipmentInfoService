using Application.Models;

namespace Application.Contracts
{
    public interface IEquipmentRepository
    {
        Task<Result<IEnumerable<Equipment>>> GetAllAsync();
        Task<Result> UpsertAsync(Equipment equipment);
    }
}