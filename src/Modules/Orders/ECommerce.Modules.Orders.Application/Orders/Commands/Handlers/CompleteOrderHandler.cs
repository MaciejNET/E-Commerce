using ECommerce.Modules.Orders.Application.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.Handlers;

internal sealed class CompleteOrderHandler : ICommandHandler<CompleteOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClock _clock;

    public CompleteOrderHandler(IOrderRepository orderRepository, IClock clock)
    {
        _orderRepository = orderRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CompleteOrder command)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        
        order.Complete(_clock.CurrentDate());
        await _orderRepository.UpdateAsync(order);
    }
}