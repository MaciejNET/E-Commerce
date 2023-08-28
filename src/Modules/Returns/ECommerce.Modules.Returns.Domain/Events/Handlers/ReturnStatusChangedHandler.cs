using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Returns.Domain.Events.Handlers;

internal sealed class ReturnStatusChangedHandler : IDomainEventHandler<ReturnStatusChanged>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly ILogger<ReturnStatusChangedHandler> _logger;
    private readonly IMessageBroker _messageBroker;

    public ReturnStatusChangedHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, ILogger<ReturnStatusChangedHandler> logger, IMessageBroker messageBroker)
    {
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _logger = logger;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(ReturnStatusChanged @event)
    {
        var order = await _orderRepository.GetAsync(@event.OrderId);
        var orderProduct = await _orderProductRepository.GetAsync(@event.OrderProductId);

        switch (@event.Status)
        {
            case ReturnStatus.Accepted:
            {
                orderProduct.Return();
                var isOrderFullyReturn = order.Products.All(x => x.IsReturn);

                if (!isOrderFullyReturn)
                {
                    order.PartlyReturn();
                    await _messageBroker.PublishAsync(new OrderPartlyReturned(order.Id));
                    _logger.LogInformation("Order with ID: '{Id}' partly return", @event.OrderId);
                }
                else
                {
                    order.Return();
                    await _messageBroker.PublishAsync(new OrderReturned(order.Id));
                    _logger.LogInformation("Order with ID: '{Id}' fully return", @event.OrderId);
                }

                break;
            }
            
            case ReturnStatus.Declined:
                _logger.LogInformation("Return for product: '{Sku}' from order with ID: '{Id}' has been declined", orderProduct.Sku, order.Id.ToString());
                break;
            
            case ReturnStatus.SendToManualCheck:
                _logger.LogInformation("Return for product: '{Sku}' from order with ID: '{Id}' has been sent to manual check", orderProduct.Sku, order.Id.ToString());
                break;
            
            default:
                _logger.LogError("Invalid return status for product: '{Sku}' from order with ID: '{Id}'", orderProduct.Sku, order.Id.ToString());
                break;
        }

        await _orderRepository.UpdateAsync(order);
        await _orderProductRepository.UpdateAsync(orderProduct);
    }
}