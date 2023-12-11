using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Discounts.Core.Events;

internal record ProductDiscountAdded(Guid ProductId, Price NewPrice) : IEvent;