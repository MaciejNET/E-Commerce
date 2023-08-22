using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External;

internal record SignedUp(Guid UserId, string Email) : IEvent;