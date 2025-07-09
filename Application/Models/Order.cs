using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public sealed class Order
    {
        [BsonId]
        public required string Id { get; set; }
        public required string Description { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required OrderStatus Status { get; set; }
    }
}