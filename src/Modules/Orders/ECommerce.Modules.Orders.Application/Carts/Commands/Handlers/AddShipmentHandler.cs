using ECommerce.Modules.Orders.Application.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class AddShipmentHandler : ICommandHandler<AddShipment>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;

    public AddShipmentHandler(ICheckoutCartRepository checkoutCartRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
    }

    public async Task HandleAsync(AddShipment command)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null)
        {
            throw new CartNotCheckedOutException(command.UserId);
        }

        var shipment = new Shipment(command.City, command.StreetName, command.StreetNumber, command.ReceiverFullName);
        
        checkoutCart.SetShipment(shipment);

        await _checkoutCartRepository.UpdateAsync(checkoutCart);
    }
}