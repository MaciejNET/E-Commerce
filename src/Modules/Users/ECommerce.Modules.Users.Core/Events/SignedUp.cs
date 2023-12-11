using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Modules.Users.Core.Events;

internal record SignedUp(Guid UserId, string Email, Currency PreferredCurrency) : IEvent;