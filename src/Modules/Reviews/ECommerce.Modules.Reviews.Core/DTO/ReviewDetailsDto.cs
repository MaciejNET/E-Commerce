namespace ECommerce.Modules.Reviews.Core.DTO;

internal class ReviewDetailsDto : ReviewDto
{
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
}