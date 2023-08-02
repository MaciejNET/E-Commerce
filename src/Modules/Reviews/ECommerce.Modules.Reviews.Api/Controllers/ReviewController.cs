using ECommerce.Modules.Reviews.Core.DTO;
using ECommerce.Modules.Reviews.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Reviews.Api.Controllers;

internal class ReviewController : BaseController
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("product/{id:guid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForProduct(Guid id)
        => Ok(await _reviewService.GetForProductAsync(id));
    
    [HttpGet("user/{id:guid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForUser(Guid id)
        => Ok(await _reviewService.GetForUserAsync(id));

    [HttpPost]
    public async Task<ActionResult> Add(ReviewDetailsDto dto)
    {
        await _reviewService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return CreatedAtAction(nameof(GetForProduct), new {id = dto.ProductId}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ReviewDetailsDto dto)
    {
        var userId = Guid.Empty;
        dto.Id = id;
        dto.UserId = userId;
        await _reviewService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userId = Guid.Empty;
        await _reviewService.DeleteAsync(id, userId);
        return NoContent();
    }
}