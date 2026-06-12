using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orderly.API.Requests.Products;
using Orderly.Application.Features.Products.Commands.CreateProduct;
using Orderly.Application.Features.Products.Commands.DeleteProduct;
using Orderly.Application.Features.Products.Commands.UpdateProduct;
using Orderly.Application.Features.Products.Queries.GetProductById;
using Orderly.Application.Features.Products.Queries.GetProducts;

namespace Orderly.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand
            {
                Id = id,
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
