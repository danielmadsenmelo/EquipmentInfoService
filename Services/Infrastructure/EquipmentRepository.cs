using Application.Contracts;
using Application.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Services.Infrastructure
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly IMongoCollection<Equipment> _equipmentCollection;

        public EquipmentRepository(IOptions<EquipmentDatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _equipmentCollection = mongoDatabase.GetCollection<Equipment>(dbSettings.Value.CollectionName);
        }
        public async Task<Result<IEnumerable<Equipment>>> GetAllAsync()
        {
            var equipments = await _equipmentCollection.Find(_ => true).ToListAsync();
            return Result<IEnumerable<Equipment>>.Success(equipments);
        }

        public async Task<Result<Equipment>> GetByIdAsync(string id)
        {
            var equipment = await _equipmentCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (equipment == null)
            {
                return Result<Equipment>.Failed($"Equipment with ID {id} not found.");
            }
            return Result<Equipment>.Success(equipment);
        }

        public async Task<Result> UpsertEquipmentAsync(Equipment equipment)
        {
            var result = await GetByIdAsync(equipment.Id);
            if (result.IsFailed)
            {
                _equipmentCollection.InsertOne(equipment);
                return Result.Success();
            }

            var existingEquipment = result.Value;
            _equipmentCollection.UpdateOne(
                e => e.Id == equipment.Id,
                Builders<Equipment>.Update
                    .Set(e => e.Status, equipment.Status)
                    .Set(e => e.Sector, equipment.Sector)
                    .Set(e => e.UpdatedAt, DateTime.UtcNow)
            );

            return Result.Success();
        }

        public async Task<Result> ReplaceOrdersAsync(Equipment equipment)
        {
            if (equipment.CurrentOrders == null || !equipment.CurrentOrders.Any())
            {
                return Result.Failed("No orders provided to update.");
            }

            var result = await GetByIdAsync(equipment.Id);
            if (result.IsFailed)
            {
                return Result.Failed($"Equipment with ID {equipment.Id} not found.");
            }

            var existingEquipment = result.Value;
            _equipmentCollection.UpdateOne(
                e => e.Id == equipment.Id,
                Builders<Equipment>.Update
                    .Set(e => e.CurrentOrders, equipment.CurrentOrders)
                    .Set(e => e.UpdatedAt, DateTime.UtcNow)
            );

            return Result.Success();
        }
    }
}