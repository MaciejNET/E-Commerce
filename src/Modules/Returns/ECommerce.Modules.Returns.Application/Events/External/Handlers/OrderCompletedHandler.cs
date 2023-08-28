using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Returns.Application.Events.External.Handlers;

internal sealed class OrderCompletedHandler : IEventHandler<OrderCompleted>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderCompletedHandler> _logger;

    public OrderCompletedHandler(IOrderRepository orderRepository, ILogger<OrderCompletedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderCompleted @event)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }
        
        order.Complete(@event.Now);
        _logger.LogInformation("Order with ID: '{Id}' has been completed", order.Id.ToString());
    }
}