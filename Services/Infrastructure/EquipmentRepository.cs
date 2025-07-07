using Application.Contracts;
using Application.Models;

namespace Services.Infrastructure
{
    public class EquipmentRepository : IEquipmentRepository
    {
        public async Task<Result<IEnumerable<Equipment>>> GetAllAsync()
        {
            // Simulate fetching data from a database or external source
            return Result<IEnumerable<Equipment>>.Success(new List<Equipment>
            {
                new Equipment
                {
                    Id = Guid.NewGuid(),
                    Status = OperationStatus.Running,
                    Sector = "Manufacturing",
                    CurrentOrders = new List<Order>
                    {
                        new Order { Id = Guid.NewGuid(), Status = OrderStatus.Scheduled },
                        new Order { Id = Guid.NewGuid(), Status = OrderStatus.InProgress }
                    }
                },
                new Equipment
                {
                    Id = Guid.NewGuid(),
                    Status = OperationStatus.WindingDown,
                    Sector = "Logistics",
                    CurrentOrders = new List<Order>()
                }
            });
        }

        public async Task<Result> UpsertAsync(Equipment equipment)
        {
            // Simulate updating or inserting the equipment in a database
            var equipments = await GetAllAsync();
            var existingEquipment = equipments.Value!.FirstOrDefault(e => e.Id == equipment.Id);
            if (existingEquipment != null)
            {
                existingEquipment.Status = equipment.Status;
                existingEquipment.Sector = equipment.Sector;
                existingEquipment.CurrentOrders = equipment.CurrentOrders;
            }
            else
            {
                equipments.Value!.Append(equipment);
            }
            return Result.Success();
        }
    }
}