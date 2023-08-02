using ECommerce.Modules.Reviews.Core.DTO;

namespace ECommerce.Modules.Reviews.Core.Services;

internal interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetForProductAsync(Guid productId);
    Task<IEnumerable<ReviewDto>> GetForUserAsync(Guid userId);
    Task AddAsync(ReviewDetailsDto dto);
    Task UpdateAsync(ReviewDetailsDto dto);
    Task DeleteAsync(Guid id, Guid userId);
}