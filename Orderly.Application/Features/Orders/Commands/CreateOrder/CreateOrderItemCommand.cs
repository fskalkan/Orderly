namespace Orderly.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderItemCommand
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
