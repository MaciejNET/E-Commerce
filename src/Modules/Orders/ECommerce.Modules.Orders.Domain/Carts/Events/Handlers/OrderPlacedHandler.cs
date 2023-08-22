using ECommerce.Modules.Orders.Domain.Orders.Entities;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Domain.Carts.Events.Handlers;

public sealed class OrderPlacedHandler : IDomainEventHandler<OrderPlaced>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderPlacedHandler> _logger;

    public OrderPlacedHandler(IOrderRepository orderRepository, ILogger<OrderPlacedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderPlaced @event)
    {
        var order = Order.CreateFromCheckout(@event.CheckoutCart, @event.Now);

        await _orderRepository.AddAsync(order);
        _logger.LogInformation("Created order with ID: '{OrderId}'", order.Id.ToString());
    }
}