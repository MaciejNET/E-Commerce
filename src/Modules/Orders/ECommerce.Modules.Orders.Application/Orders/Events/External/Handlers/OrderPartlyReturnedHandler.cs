using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Application.Orders.Events.External.Handlers;

internal sealed class OrderPartlyReturnedHandler : IEventHandler<OrderPartlyReturned>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderPartlyReturnedHandler> _logger;

    public OrderPartlyReturnedHandler(IOrderRepository orderRepository, ILogger<OrderPartlyReturnedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderPartlyReturned @event)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }
        
        order.PartlyReturn();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' partly return", order.Id.Value);
    }
}