using ECommerce.Modules.Orders.Application.Shared.DTO;
using ECommerce.Modules.Orders.Domain.Shared.Entities;

namespace ECommerce.Modules.Orders.Application.Carts.DTO;

public record CheckoutCartDto(Guid Id,
    PaymentMethod PaymentMethod,
    ShipmentDto Shipment,
    DiscountDto Discount,
    IEnumerable<CartItemDto> CartItems);