using ECommerce.Modules.Orders.Application.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.Handlers;

internal sealed class StartProcessingOrderHandler : ICommandHandler<StartProcessingOrder>
{
    private readonly IOrderRepository _orderRepository;

    public StartProcessingOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(StartProcessingOrder command)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        
        order.StartProcessing();
        await _orderRepository.UpdateAsync(order);
    }
}