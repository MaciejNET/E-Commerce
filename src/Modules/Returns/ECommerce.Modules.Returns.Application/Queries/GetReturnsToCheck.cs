using ECommerce.Modules.Returns.Application.DTO;
using ECommerce.Shared.Abstractions.Queries;

namespace ECommerce.Modules.Returns.Application.Queries;

public class GetReturnsToCheck : IQuery<IEnumerable<ReturnDto>>
{
}