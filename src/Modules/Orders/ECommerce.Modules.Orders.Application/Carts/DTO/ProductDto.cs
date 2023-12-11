using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.DTO;

 public record ProductDto(Guid Id, string Name, string Sku, Price StandardPrice, Price? DiscountedPrice);