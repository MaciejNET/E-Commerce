using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Repositories;

public interface ICartItemRepository
{
    Task<CartItem> GetAsync(EntityId id);
    Task AddAsync(CartItem item);
    Task UpdateAsync(CartItem item);
    Task DeleteAsync(CartItem item);
    Task DeleteRangeAsync(IEnumerable<CartItem> items);
}