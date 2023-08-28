using ECommerce.Modules.Orders.Application.Orders.Events;
using ECommerce.Modules.Orders.Application.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.Handlers;

internal sealed class SendOrderHandler : ICommandHandler<SendOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMessageBroker _messageBroker;

    public SendOrderHandler(IOrderRepository orderRepository, IMessageBroker messageBroker)
    {
        _orderRepository = orderRepository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(SendOrder command)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        
        order.Send();
        await _orderRepository.UpdateAsync(order);
        await _messageBroker.PublishAsync(new OrderSent(order.Id));
    }
}