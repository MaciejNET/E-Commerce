using ECommerce.Modules.Orders.Application.Carts.Commands;
using ECommerce.Modules.Orders.Application.Carts.DTO;
using ECommerce.Modules.Orders.Application.Carts.Queries;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Contexts;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Orders.Api.Controllers;

[Authorize(Policy = Policy)]
internal class CartController : BaseController
{
    private const string Policy = "carts";
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;

    public CartController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CartDto>> Get()
    {
        var id = _context.Identity.Id;

        return await _queryDispatcher.QueryAsync(new GetCart {UserId = id});
    }

    [HttpPost("add-product")]
    public async Task<ActionResult> AddProduct(AddProductToCart command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("remove-product")]
    public async Task<ActionResult> RemoveProduct(RemoveProductFromCart command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPut("{productId:guid}/increase-quantity")]
    public async Task<ActionResult> IncreaseQuantity(Guid productId, int quantity)
    {
        var command = new IncreaseCartItemQuantity(productId, quantity);

        await _commandDispatcher.SendAsync(command);

        return NoContent();
    }
    
    [HttpPut("{productId:guid}/decrease-quantity")]
    public async Task<ActionResult> DecreaseQuantity(Guid productId, int quantity)
    {
        var command = new DecreaseCartItemQuantity(productId, quantity);

        await _commandDispatcher.SendAsync(command);

        return NoContent();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult> Checkout()
    {
        var id = _context.Identity.Id;
        var command = new CheckoutCart(id); 
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Clear()
    {
        var id = _context.Identity.Id;
        var command = new ClearCart(id);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}