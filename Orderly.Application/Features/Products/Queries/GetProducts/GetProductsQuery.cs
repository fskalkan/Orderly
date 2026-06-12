using MediatR;

namespace Orderly.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<List<GetProductsResponse>>
    {
    }
}
