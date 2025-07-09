using Application.Models;

namespace Workers.Consumers
{
    public record EquipmentInfoUpdateEvent(
        EventType EventType,
        string EquipmentId,
        OperationStatus? Status,
        string? Sector,
        IEnumerable<OrderEvent>? CurrentOrders = null
        );

    public enum EventType
    {
        Undefined = 0,
        EquipmentStatusUpdate = 1,
        OrderUpdate = 2
    }

    public record OrderEvent(string OrderId, string Description, OrderStatus Status);
}
