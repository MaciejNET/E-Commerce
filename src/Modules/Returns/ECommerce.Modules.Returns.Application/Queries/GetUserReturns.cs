using ECommerce.Modules.Returns.Application.DTO;
using ECommerce.Shared.Abstractions.Queries;

namespace ECommerce.Modules.Returns.Application.Queries;

public class GetUserReturns : IQuery<IEnumerable<ReturnDto>>
{
    public Guid Id { get; set; }
}