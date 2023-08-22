namespace ECommerce.Modules.Orders.Application.Shared.DTO;

public record ShipmentDto(string City, string StreetName, int StreetNumber, string ReceiverFullName);