using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public sealed class Equipment
    {
        [BsonId]
        public required string Id { get; set; }
        public required OperationStatus Status { get; set; }
        public required string Sector { get; set; } = string.Empty;
        public IEnumerable<Order> CurrentOrders { get; set; } = [];
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
