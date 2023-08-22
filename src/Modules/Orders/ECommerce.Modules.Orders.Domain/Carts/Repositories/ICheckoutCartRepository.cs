using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Repositories;

public interface ICheckoutCartRepository
{
    Task<CheckoutCart> GetAsync(AggregateId id);
    Task<CheckoutCart> GetAsync(UserId userId);
    Task AddAsync(CheckoutCart checkoutCart);
    Task UpdateAsync(CheckoutCart checkoutCart);
    Task DeleteAsync(CheckoutCart checkoutCart);
}