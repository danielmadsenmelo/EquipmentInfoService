
namespace Application.Models
{
    public sealed class Equipment
    {
        public Guid Id { get; set; }
        public OperationStatus Status { get; set; }
        public string Sector { get; set; } = string.Empty;
        public IEnumerable<Order> CurrentOrders { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
