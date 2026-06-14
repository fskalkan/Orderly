namespace Orderly.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public List<GetOrderByIdItemResponse> Items { get; set; } = new();
    }
}