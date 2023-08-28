using ECommerce.Modules.Returns.Application.DTO;
using ECommerce.Modules.Returns.Application.Queries;
using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Returns.Infrastructure.EF.Queries;

internal sealed class GetReturnsToCheckHandler : IQueryHandler<GetReturnsToCheck, IEnumerable<ReturnDto>>
{
    private readonly ReturnsDbContext _context;

    public GetReturnsToCheckHandler(ReturnsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReturnDto>> HandleAsync(GetReturnsToCheck query)
        => await _context.Returns
            .Include(x => x.Order)
            .Include(x => x.OrderProduct)
            .Where(x => x.ReturnStatus == ReturnStatus.SendToManualCheck)
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