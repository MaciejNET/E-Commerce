using ECommerce.Modules.Orders.Application.Orders.DTO;
using ECommerce.Modules.Orders.Application.Orders.Queries;
using ECommerce.Modules.Orders.Domain.Orders.Entities;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
{
    private readonly DbSet<Order> _orders;

    public GetOrderHandler(OrdersDbContext context)
    {
        _orders = context.Orders;
    }
    
    public async Task<OrderDto> HandleAsync(GetOrder query)
    {
        var order = await _orders
            .Include(x => x.Lines)
            .SingleOrDefaultAsync(x => x.Id == query.Id);

        return order.AsDto();
    }
}