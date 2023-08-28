using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Returns.Domain.Repositories;

public interface IReturnRepository
{

    Task<Return> GetAsync(AggregateId id);
    Task<IEnumerable<Return>> BrowsAsync();
    Task AddAsync(Return @return);
    Task UpdateAsync(Return @return);
    Task DeleteAsync(Return @return);
}