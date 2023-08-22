using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Repositories;

public interface ICartRepository
{
    Task<Cart> GetAsync(AggregateId id);
    Task<Cart> GetAsync(UserId id);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
}