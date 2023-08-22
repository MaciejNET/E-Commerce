using ECommerce.Modules.Orders.Application.Orders.DTO;
using ECommerce.Shared.Abstractions.Queries;

namespace ECommerce.Modules.Orders.Application.Orders.Queries;

public sealed class BrowseOrders : IQuery<IEnumerable<OrderDto>>
{
    public Guid? UserId { get; set; }
    public bool? IsCompleted { get; set; }
}