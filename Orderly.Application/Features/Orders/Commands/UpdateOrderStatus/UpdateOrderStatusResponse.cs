namespace Orderly.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusResponse
    {
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? UpdatedAt { get; set; }
    }
}