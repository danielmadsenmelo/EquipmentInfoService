namespace Application.Models
{
    public sealed class Order
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
    }
}