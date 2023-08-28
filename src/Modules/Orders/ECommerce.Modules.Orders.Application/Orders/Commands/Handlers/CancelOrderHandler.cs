using ECommerce.Modules.Orders.Application.Orders.Events;
using ECommerce.Modules.Orders.Application.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.Handlers;

internal sealed class CancelOrderHandler : ICommandHandler<CancelOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMessageBroker _messageBroker;

    public CancelOrderHandler(IOrderRepository orderRepository, IMessageBroker messageBroker)
    {
        _orderRepository = orderRepository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(CancelOrder command)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        
        order.Cancel();
        await _orderRepository.UpdateAsync(order);
        await _messageBroker.PublishAsync(new OrderCanceled(order.Id));
    }
}