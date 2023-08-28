using ECommerce.Modules.Orders.Application.Carts.Events;
using ECommerce.Modules.Orders.Application.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class PlaceOrderHandler : ICommandHandler<PlaceOrder>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IClock _clock;
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly IMessageBroker _messageBroker;

    public PlaceOrderHandler(ICheckoutCartRepository checkoutCartRepository, IClock clock, IDomainEventDispatcher dispatcher, IMessageBroker messageBroker)
    {
        _checkoutCartRepository = checkoutCartRepository;
        _clock = clock;
        _dispatcher = dispatcher;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(PlaceOrder command)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null)
        {
            throw new CheckoutCartNotFoundException(command.UserId);
        }

        var orderId = new AggregateId();
        checkoutCart.PlaceOrder(_clock, orderId);
        await _dispatcher.DispatchAsync(checkoutCart.Events.ToArray());

        var integrationEvents = (IEnumerable<IMessage>)checkoutCart.Events.Select(x => x switch
        {
            Domain.Carts.Events.ProductBought p => new ProductBought(p.Id, p.Quantity),
            _ => null
        });

        await _messageBroker.PublishAsync(integrationEvents.ToArray());
        await _messageBroker.PublishAsync(new OrderPlaced(
            orderId,
            _clock.CurrentDate(),
            checkoutCart.Items.Select(x => x.Product.Sku)));
        await _checkoutCartRepository.DeleteAsync(checkoutCart);
    }
}