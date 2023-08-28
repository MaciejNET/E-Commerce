using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Returns.Application.Events.External.Handlers;

internal sealed class OrderSentHandler : IEventHandler<OrderSent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderSentHandler> _logger;

    public OrderSentHandler(IOrderRepository orderRepository, ILogger<OrderSentHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderSent @event)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }
        
        order.Send();
        _logger.LogInformation("Order with ID: '{Id}' has been sent", order.Id.ToString());
    }
}