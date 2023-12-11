using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Entities;

public sealed class CheckoutCartItem
{
    public EntityId Id { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public Price Price { get; private set; }
    public Price? DiscountedPrice { get; private set; }

    internal CheckoutCartItem(int quantity, Product product, Price price)
    {
        Id = new EntityId(Guid.NewGuid());
        if (quantity < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        Quantity = quantity;
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Price = price;
    }

    internal bool ApplyDiscount(Discount discount)
    {
        if (discount.Type == DiscountType.Product && !discount.Products.Contains(Product))
        {
            return false;
        }

        DiscountedPrice = new Price(Price.Amount * (1 - discount.Percentage / 100M), Price.Currency);

        return true;
    }

    private CheckoutCartItem()
    {
    }
}