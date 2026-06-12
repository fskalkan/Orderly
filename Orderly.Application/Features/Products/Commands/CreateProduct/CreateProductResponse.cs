namespace Orderly.Application.Features.Products.Commands.CreateProduct;

public class CreateProductResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}