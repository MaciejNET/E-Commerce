using ECommerce.Modules.Discounts.Core.DTO;
using ECommerce.Modules.Discounts.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Discounts.Api.Controllers;

internal class DiscountCodeController : BaseController
{
    private readonly IDiscountCodeService _discountCodeService;

    public DiscountCodeController(IDiscountCodeService discountCodeService)
    {
        _discountCodeService = discountCodeService;
    }

    [HttpPost]
    public async Task<ActionResult> Add(DiscountCodeDto dto)
    {
        await _discountCodeService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Deleted(Guid id)
    {
        await _discountCodeService.DeleteAsync(id);

        return NoContent();
    }
}