using ECommerce.Modules.Orders.Application.Orders.Commands;
using ECommerce.Modules.Orders.Application.Orders.DTO;
using ECommerce.Modules.Orders.Application.Orders.Queries;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Contexts;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Orders.Api.Controllers;

[Authorize(Policy = Policy)]
internal class OrderController : BaseController
{
    private const string Policy = "orders";
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;

    public OrderController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Get(Guid id)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetOrder {Id = id}));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> Browse(bool? isCompleted)
    {
        var role = _context.Identity.Role;

        var query = new BrowseOrders
        {
            UserId = role == "user" ? _context.Identity.Id : null,
            IsCompleted = isCompleted
        };

        return Ok(await _queryDispatcher.QueryAsync(query));
    }

    [HttpPost("{id:guid}/start-processing")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> StartProcessing(Guid id)
    {
        await _commandDispatcher.SendAsync(new StartProcessingOrder(id));

        return Ok();
    }
    
    [HttpPost("{id:guid}/send")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Send(Guid id)
    {
        await _commandDispatcher.SendAsync(new SendOrder(id));

        return Ok();
    }
    
    [HttpPost("{id:guid}/complete")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Complete(Guid id)
    {
        await _commandDispatcher.SendAsync(new CompleteOrder(id));

        return Ok();
    }
    
    [HttpPost("{id:guid}/cancel")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Cancel(Guid id)
    {
        await _commandDispatcher.SendAsync(new CancelOrder(id));

        return Ok();
    }
}