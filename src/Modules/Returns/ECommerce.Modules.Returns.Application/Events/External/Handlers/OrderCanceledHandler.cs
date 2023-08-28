using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Returns.Application.Events.External.Handlers;

internal sealed class OrderCanceledHandler : IEventHandler<OrderCanceled>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderCanceledHandler> _logger;

    public OrderCanceledHandler(IOrderRepository orderRepository, ILogger<OrderCanceledHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderCanceled @event)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }
        
        order.Cancel();
        _logger.LogInformation("Order with ID: '{Id}' has been canceled", order.Id.ToString());
    }
}