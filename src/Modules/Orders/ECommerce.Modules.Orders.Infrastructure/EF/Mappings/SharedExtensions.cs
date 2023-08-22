using ECommerce.Modules.Orders.Application.Shared.DTO;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Mappings;

public static class SharedExtensions
{
    internal static ShipmentDto AsDto(this Shipment shipment)
        => new(
            City: shipment.City,
            StreetName: shipment.StreetName,
            StreetNumber: shipment.StreetNumber,
            ReceiverFullName: shipment.ReceiverFullName
        );
}