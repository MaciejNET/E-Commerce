using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.External.Handlers;

internal sealed class OrderReturnedHandler : IEventHandler<OrderReturned>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderReturnedHandler> _logger;

    public OrderReturnedHandler(IOrderRepository orderRepository, ILogger<OrderReturnedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderReturned @event)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }
        
        order.Return();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' return", order.Id.Value);
    }
}