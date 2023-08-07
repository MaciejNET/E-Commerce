using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Discounts.Core.BackgroundServices;

internal class ProductDiscountExpirationService : BackgroundService
{
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<ProductDiscountExpirationService> _logger;
    private readonly IClock _clock;
    private readonly IServiceProvider _serviceProvider;

    public ProductDiscountExpirationService(IMessageBroker messageBroker, ILogger<ProductDiscountExpirationService> logger, IClock clock, IServiceProvider serviceProvider)
    {
        _messageBroker = messageBroker;
        _logger = logger;
        _clock = clock;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var productDiscountRepository = scope.ServiceProvider.GetRequiredService<IProductDiscountRepository>();
        var currentDate = _clock.CurrentDate();
        var expiredProducts = await productDiscountRepository.GetExpiredProductsAsync(currentDate);

        foreach (var expiredProduct in expiredProducts)
        {
            await _messageBroker.PublishAsync(new ProductDiscountExpired(expiredProduct.ProductId));
            _logger.LogInformation("Expired discount for product with ID: '{Id}' has been processed", expiredProduct.ProductId);
        }

        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
    }
}