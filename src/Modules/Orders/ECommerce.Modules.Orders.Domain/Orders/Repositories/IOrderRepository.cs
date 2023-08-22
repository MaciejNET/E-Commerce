using ECommerce.Modules.Orders.Domain.Orders.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Orders.Repositories;

public interface IOrderRepository
{
    Task<Order> GetAsync(AggregateId id);
    Task<IEnumerable<Order>> BrowseAsync(UserId id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}