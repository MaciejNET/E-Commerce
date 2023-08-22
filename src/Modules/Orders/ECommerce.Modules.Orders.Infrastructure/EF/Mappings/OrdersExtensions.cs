using ECommerce.Modules.Orders.Application.Orders.DTO;
using ECommerce.Modules.Orders.Domain.Orders.Entities;
using ECommerce.Modules.Orders.Domain.Orders.ValueObjects;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Mappings;

public static class OrdersExtensions
{
    internal static OrderLineDto AsDto(this OrderLine line)
        => new(
            OrderLineNumber: line.OrderLineNumber,
            Sku: line.Sku,
            Name: line.Name,
            UnitPrice: line.UnitPrice,
            Quantity: line.Quantity
        );

    public static OrderDto AsDto(this Order order)
        => new(
            Id: order.Id,
            UserId: order.UserId,
            Lines: order.Lines.Select(AsDto),
            Shipment: order.Shipment.AsDto(),
            Payment: order.PaymentMethod,
            PlaceDate: order.PlaceDate,
            Status: order.Status
        );
}