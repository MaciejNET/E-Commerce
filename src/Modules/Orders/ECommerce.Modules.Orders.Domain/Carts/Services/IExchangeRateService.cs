using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Services;

public interface IExchangeRateService
{
    Task<Price> Exchange(Price price, Currency currency);
}