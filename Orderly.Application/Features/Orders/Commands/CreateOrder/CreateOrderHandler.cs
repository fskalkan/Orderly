using MediatR;
using Orderly.Application.Common.Exceptions;
using Orderly.Application.Interfaces;
using Orderly.Application.Interfaces.Repositories;
using Orderly.Domain.Entities;

namespace Orderly.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                throw new NotFoundException($"Customer with ID {request.CustomerId} not found.");
            }

            var productIds = request.Items
                .Select(i => i.ProductId)
                .Distinct()
                .ToList();

            var products = await _productRepository.GetByIdsForUpdateAsync(productIds, cancellationToken);

            if (products.Count != productIds.Count)
            {
                throw new NotFoundException("One or more products were not found.");
            }

            var order = new Order
            {
                CustomerId = request.CustomerId
            };

            foreach (var item in request.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);

                if (product.StockQuantity < item.Quantity)
                {
                    throw new BadRequestException($"Not enough stock for product: {product.Name}");
                }

                var unitPrice = product.Price;
                var totalPrice = unitPrice * item.Quantity;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = totalPrice
                };

                order.OrderItems.Add(orderItem);

                product.StockQuantity -= item.Quantity;
            }

            order.TotalAmount = order.OrderItems.Sum(oi => oi.TotalPrice);

            await _orderRepository.AddAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateOrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                OrderDate = order.OrderDate,
                Items = order.OrderItems.Select(oi =>
                {
                    var product = products.First(p => p.Id == oi.ProductId);

                    return new CreateOrderItemResponse
                    {
                        ProductId = oi.ProductId,
                        ProductName = product.Name,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        TotalPrice = oi.TotalPrice
                    };
                }).ToList()
            };
        }
    }
}