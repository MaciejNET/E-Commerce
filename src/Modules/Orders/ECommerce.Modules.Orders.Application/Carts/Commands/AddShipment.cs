using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record AddShipment(Guid UserId, string City, string StreetName, int StreetNumber, string ReceiverFullName) : ICommand;