using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Discounts.Core.BackgroundServices;

internal class ProductDiscountAddedService : BackgroundService
{
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<ProductDiscountAddedService> _logger;
    private readonly IClock _clock;
    private readonly IServiceProvider _serviceProvider;

    public ProductDiscountAddedService(IMessageBroker messageBroker, ILogger<ProductDiscountAddedService> logger, IClock clock, IServiceProvider serviceProvider)
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
            var productDiscountRepository = scope.ServiceProvider.GetRequiredService<IProductDiscountRepository>();
            var currentDate = _clock.CurrentDate();
            var newDiscounts = await productDiscountRepository.GetNewlyAddedDiscountsAsync(currentDate);
            
            foreach (var newDiscount in newDiscounts)
            {
                await _messageBroker.PublishAsync(new ProductDiscountAdded(newDiscount.ProductId,newDiscount.NewPrice));
                _logger.LogInformation("New discount added for product with ID: '{ProductId}'", newDiscount.ProductId);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}