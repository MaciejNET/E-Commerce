using ECommerce.Modules.Orders.Application.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.Handlers;

internal sealed class SendOrderHandler : ICommandHandler<SendOrder>
{
    private readonly IOrderRepository _orderRepository;

    public SendOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
    }
}