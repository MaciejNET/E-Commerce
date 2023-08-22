using ECommerce.Modules.Orders.Application.Carts.DTO;
using ECommerce.Modules.Orders.Application.Carts.Queries;
using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class GetCartHandler : IQueryHandler<GetCart, CartDto>
{
    private readonly DbSet<Cart> _carts;

    public GetCartHandler(OrdersDbContext context)
    {
        _carts = context.Carts;
    }

    public async Task<CartDto> HandleAsync(GetCart query)
    {
        var cart = await _carts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.UserId == query.UserId);

        return cart.AsDto();
    }
}