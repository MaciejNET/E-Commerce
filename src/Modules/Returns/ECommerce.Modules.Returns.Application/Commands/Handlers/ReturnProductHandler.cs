using ECommerce.Modules.Returns.Application.Exceptions;
using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Policies;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Returns.Application.Commands.Handlers;

internal sealed class ReturnProductHandler : ICommandHandler<ReturnProduct>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IReturnPolicyFactory _returnPolicyFactory;
    private readonly IReturnRepository _returnRepository;
    private readonly IDomainEventDispatcher _dispatcher;
    
    private readonly IClock _clock;

    public ReturnProductHandler(IOrderRepository orderRepository, IReturnPolicyFactory returnPolicyFactory, IClock clock, IReturnRepository returnRepository, IDomainEventDispatcher dispatcher)
    {
        _orderRepository = orderRepository;
        _returnPolicyFactory = returnPolicyFactory;
        _clock = clock;
        _returnRepository = returnRepository;
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(ReturnProduct command)
    {
        var order = await _orderRepository.GetAsync(command.OrderId);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        var products = order.Products.Where(x => x.Sku == command.Sku && !x.IsReturn).ToList();

        if (products.Count == 0)
        {
            throw new NoReturnableProductsException(command.Sku);
        }

        var product = products.FirstOrDefault();
        
        var @return = Return.Create(Guid.NewGuid(), command.UserId, order, product, command.Type);
        await _returnRepository.AddAsync(@return);

        var policy = _returnPolicyFactory.Get(@return.Type);
        
        policy.Return(@return, _clock);
        await _dispatcher.DispatchAsync(@return.Events.ToArray());
        await _returnRepository.UpdateAsync(@return);
    }
}