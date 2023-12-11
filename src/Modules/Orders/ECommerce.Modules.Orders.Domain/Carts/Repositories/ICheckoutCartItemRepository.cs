using ECommerce.Modules.Orders.Domain.Carts.Entities;

namespace ECommerce.Modules.Orders.Domain.Carts.Repositories;

public interface ICheckoutCartItemRepository
{
    Task AddAsync(CheckoutCartItem item);
    Task AddRangeAsync(IEnumerable<CheckoutCartItem> items);
    Task DeleteRangeAsync(IEnumerable<CheckoutCartItem> items);
}