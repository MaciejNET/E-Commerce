using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Discounts.Core.BackgroundServices;

internal class DiscountCodeExpirationService : BackgroundService
{
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<DiscountCodeExpirationService> _logger;
    private readonly IClock _clock;
    private readonly IServiceProvider _serviceProvider;

    public DiscountCodeExpirationService(IMessageBroker messageBroker, ILogger<DiscountCodeExpirationService> logger, IClock clock, IServiceProvider serviceProvider)
    {
        _messageBroker = messageBroker;
        _logger = logger;
        _clock = clock;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {


            using var scope = _serviceProvider.CreateScope();
            var discountCodeRepository = scope.ServiceProvider.GetRequiredService<IDiscountCodeRepository>();
            var currentDate = _clock.CurrentDate();
            var expiredCodes = await discountCodeRepository.GetExpiredCodesAsync(currentDate);

            foreach (var expiredCode in expiredCodes)
            {
                await _messageBroker.PublishAsync(new DiscountCodeExpired(expiredCode.Id));
                _logger.LogInformation("Expired discount code: '{Code}' with ID: '{Id}' has been processed",
                    expiredCode.Code, expiredCode.Id);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}