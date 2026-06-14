namespace Orderly.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public List<CreateOrderItemResponse> Items { get; set; } = new();
    }
}