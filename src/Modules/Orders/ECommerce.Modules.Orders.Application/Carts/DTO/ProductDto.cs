namespace ECommerce.Modules.Orders.Application.Carts.DTO;

public record ProductDto(Guid Id, string Name, string Sku, decimal StandardPrice, decimal? DiscountedPrice);