using ECommerce.Modules.Returns.Application.Commands;
using ECommerce.Modules.Returns.Application.DTO;
using ECommerce.Modules.Returns.Application.Queries;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Contexts;
using ECommerce.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Returns.Api.Controllers;

[Authorize(Policy = Policy)]
internal class ReturnController : BaseController
{
    private const string Policy = "returns";
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;

    public ReturnController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReturnDto>>> GetUserReturns()
    {
        var id = _context.Identity.Id;

        return Ok(await _queryDispatcher.QueryAsync(new GetUserReturns {Id = id}));
    }
    
    [HttpGet("to-check")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<IEnumerable<ReturnDto>>> GetReturnsToCheck()
    {
        return Ok(await _queryDispatcher.QueryAsync(new GetReturnsToCheck()));
    }

    [HttpPost]
    public async Task<ActionResult> Return(ReturnProduct command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);

        return Ok();
    }
    
    [HttpPost("accept")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Accept(AcceptReturn command)
    {
        await _commandDispatcher.SendAsync(command);

        return Ok();
    }
    
    [HttpPost("decline")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Decline(DeclineReturn command)
    {
        await _commandDispatcher.SendAsync(command);

        return Ok();
    }
}