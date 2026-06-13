using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orderly.Application.Features.Customers.Commands.CreateCustomer;
using Orderly.Application.Features.Customers.Queries.GetCustomers;
using Orderly.Application.Features.Customers.Queries.GetCustomerById;
using Orderly.API.Requests.Customers;
using Orderly.Application.Features.Customers.Commands.UpdateCustomer;
using Orderly.Application.Features.Customers.Commands.DeleteCustomer;

namespace Orderly.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken)
        {
            var query = new GetCustomersQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateCustomerCommand
            {
                Id = id,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteCustomerCommand { Id = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
