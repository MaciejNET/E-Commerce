using ECommerce.Modules.Products.Core.DTO;
using ECommerce.Modules.Products.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Products.Api.Controllers;

internal class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDetailsDto>>> Browse()
        => Ok(await _categoryService.BrowseAsync());

    [HttpPost]
    public async Task<ActionResult> Add(CategoryDto dto)
    {
        await _categoryService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return CreatedAtAction(nameof(Browse), null, null);
    }
}