using Application.Models;

namespace Application.Contracts
{
    public interface IEquipmentRepository
    {
        Task<Result<IEnumerable<Equipment>>> GetAllAsync();
        Task<Result<Equipment>> GetByIdAsync(string id);
        Task<Result> UpsertEquipmentAsync(Equipment equipment);
        Task<Result> ReplaceOrdersAsync(Equipment equipment);
    }
}