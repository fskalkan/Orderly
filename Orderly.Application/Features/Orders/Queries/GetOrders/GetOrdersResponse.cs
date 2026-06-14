namespace Orderly.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public List<GetOrdersItemResponse> Items { get; set; } = new();
    }
}