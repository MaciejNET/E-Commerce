using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.DTO;

public record CheckoutCartItemDto(Guid Id, int Quantity, Price Price, Price? DiscountedPrice);