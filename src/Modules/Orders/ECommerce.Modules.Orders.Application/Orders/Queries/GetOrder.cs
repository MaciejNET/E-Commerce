using ECommerce.Modules.Orders.Application.Orders.DTO;
using ECommerce.Shared.Abstractions.Queries;

namespace ECommerce.Modules.Orders.Application.Orders.Queries;

public sealed class GetOrder : IQuery<OrderDto>
{
    public Guid Id { get; set; }
}