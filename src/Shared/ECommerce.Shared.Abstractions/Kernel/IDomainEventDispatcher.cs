namespace ECommerce.Shared.Abstractions.Kernel;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(params IDomainEvent[] events);
}