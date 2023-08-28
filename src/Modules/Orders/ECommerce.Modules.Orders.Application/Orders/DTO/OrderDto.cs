using ECommerce.Modules.Orders.Application.Shared.DTO;
using ECommerce.Modules.Orders.Domain.Orders.Entities;
using ECommerce.Modules.Orders.Domain.Shared.Enums;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Modules.Orders.Application.Orders.DTO;

public record OrderDto(
    Guid Id,
    Guid UserId,
    IEnumerable<OrderLineDto> Lines,
    ShipmentDto Shipment,
    PaymentMethod Payment,
    DateTime PlaceDate,
    OrderStatus Status);

public record OrderLineDto(
    int OrderLineNumber,
    string Sku,
    string Name,
    decimal UnitPrice,
    int Quantity);