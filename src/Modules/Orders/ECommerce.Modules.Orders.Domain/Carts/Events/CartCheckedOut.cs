using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events;

public record CartCheckedOut(Cart Cart, List<CheckoutCartItem> Items) : IDomainEvent;