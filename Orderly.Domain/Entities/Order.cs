using Orderly.Domain.Common;
using Orderly.Domain.Enums;

namespace Orderly.Domain.Entities;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}