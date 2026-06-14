using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orderly.API.Requests.Orders;
using Orderly.Application.Features.Orders.Commands.CreateOrder;
using Orderly.Application.Features.Orders.Commands.UpdateOrderStatus;
using Orderly.Application.Features.Orders.Queries.GetOrderById;
using Orderly.Application.Features.Orders.Queries.GetOrders;
using Orderly.Domain.Enums;

namespace Orderly.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var query = new GetOrdersQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetOrderByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int id, [FromBody] UpdateOrderStatusRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateOrderStatusCommand
            {
                Id = id,
                Status = (OrderStatus)request.Status
            };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}