using ECommerce.Modules.Orders.Application.Carts.DTO;
using ECommerce.Modules.Orders.Domain.Carts.Entities;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Mappings;

public static class CartsExtensions
{
    internal static ProductDto AsDto(this Product product)
        => new(
            Id: product.Id,
            Name: product.Name,
            Sku: product.Sku,
            StandardPrice: product.StandardPrice,
            DiscountedPrice: product.DiscountedPrice
        );

    internal static CartItemDto AsDto(this CartItem cartItem)
        => new(
            Id: cartItem.Id,
            Quantity: cartItem.Quantity,
            Product: cartItem.Product.AsDto()
        );

    internal static CheckoutCartItemDto AsDto(this CheckoutCartItem checkoutCartItem)
        => new(
            Id: checkoutCartItem.Id,
            Quantity: checkoutCartItem.Quantity,
            Price: checkoutCartItem.Price,
            DiscountedPrice: checkoutCartItem.DiscountedPrice
        );

    public static CartDto AsDto(this Cart cart)
        => new(
            Id: cart.Id,
            CartItems: cart.Items.Select(AsDto)
        );

    internal static DiscountDto AsDto(this Discount discount)
        => new(
            Id: discount.Id,
            Code: discount.Code,
            Percentage: discount.Percentage
        );

    public static CheckoutCartDto AsDto(this CheckoutCart checkoutCart)
        => new(
            Id: checkoutCart.Id,
            PaymentMethod: checkoutCart.Payment,
            Shipment: checkoutCart.Shipment.AsDto(),
            Discount: checkoutCart.Discount.AsDto(),
            CartItems: checkoutCart.Items.Select(AsDto)
        );
}