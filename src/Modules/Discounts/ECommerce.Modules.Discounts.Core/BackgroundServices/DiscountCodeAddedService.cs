using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Discounts.Core.BackgroundServices;

internal class DiscountCodeAddedService : BackgroundService
{
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<DiscountCodeAddedService> _logger;
    private readonly IClock _clock;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(60);

    public DiscountCodeAddedService(IMessageBroker messageBroker, ILogger<DiscountCodeAddedService> logger, IClock clock, IServiceProvider serviceProvider)
    {
        _messageBroker = messageBroker;
        _logger = logger;
        _clock = clock;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();
            var productDiscountRepository = scope.ServiceProvider.GetRequiredService<IDiscountCodeRepository>();
            var currentDate = _clock.CurrentDate();
            var newDiscountCodes = await productDiscountRepository.GetNewlyAddedDiscountCodesAsync(currentDate);
            
            foreach (var newDiscountCode in newDiscountCodes)
            {
                await _messageBroker.PublishAsync(new DiscountCodeAdded(
                    newDiscountCode.Id,
                    newDiscountCode.Code,
                    newDiscountCode.Percentage, newDiscountCode.Products.Select(x => x.Id).ToList()));
                _logger.LogInformation("New discount code: '{Code}' added", newDiscountCode.Code);
            }
        }
    }
}