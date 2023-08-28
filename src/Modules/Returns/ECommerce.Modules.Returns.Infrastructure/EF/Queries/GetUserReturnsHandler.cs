using ECommerce.Modules.Returns.Application.DTO;
using ECommerce.Modules.Returns.Application.Queries;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Returns.Infrastructure.EF.Queries;

internal sealed class GetUserReturnsHandler : IQueryHandler<GetUserReturns, IEnumerable<ReturnDto>>
{
    private readonly ReturnsDbContext _context;

    public GetUserReturnsHandler(ReturnsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReturnDto>> HandleAsync(GetUserReturns query)
        => await _context.Returns
            .Include(x => x.Order)
            .Include(x => x.OrderProduct)
            .Where(x => x.UserId == query.Id)
            .Select(x =>
                new ReturnDto
                (
                    x.Id,
                    x.UserId,
                    x.Order.Id,
                    x.OrderProduct.Sku,
                    x.Type,
                    x.ReturnStatus
                ))
            .ToListAsync();
}