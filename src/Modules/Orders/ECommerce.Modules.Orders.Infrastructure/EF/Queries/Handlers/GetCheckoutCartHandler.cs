using ECommerce.Modules.Orders.Application.Carts.DTO;
using ECommerce.Modules.Orders.Application.Carts.Queries;
using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class GetCheckoutCartHandler : IQueryHandler<GetCheckoutCart, CheckoutCartDto>
{
    private readonly DbSet<CheckoutCart> _checkoutCarts;

    public GetCheckoutCartHandler(OrdersDbContext context)
    {
        _checkoutCarts = context.CheckoutCarts;
    }

    public async Task<CheckoutCartDto> HandleAsync(GetCheckoutCart query)
    {
        var checkoutCart = await _checkoutCarts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.UserId == query.UserId);

        return checkoutCart.AsDto();
    }
}