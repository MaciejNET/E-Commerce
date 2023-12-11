using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External;

internal record SignedUp(Guid UserId, string Email, Currency PreferredCurrency) : IEvent;