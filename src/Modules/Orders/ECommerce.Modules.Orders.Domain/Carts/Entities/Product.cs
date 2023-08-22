using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Entities;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public decimal StandardPrice { get; private set; }
    public decimal? DiscountedPrice { get; private set; }
    public int StockQuantity { get; private set; }

    public Product(AggregateId id, string name, string sku, decimal standardPrice, int stockQuantity, decimal? discountedPrice = null)
    {
        Id = id;
        Name = name;
        Sku = sku;
        StandardPrice = standardPrice;
        DiscountedPrice = discountedPrice;
        StockQuantity = stockQuantity;
    }

    public void AddStock(int quantity)
    {
        StockQuantity += quantity;
        IncrementVersion();
    }

    public void DecreaseStock(int quantity)
    {
        if (StockQuantity - quantity < 0)
        {
            throw new InvalidProductStockQuantityException();
        }
        
        StockQuantity -= quantity;
        IncrementVersion();
    }

    public void SetPrice(decimal price)
    {
        StandardPrice = price;
        IncrementVersion();
    }

    public void SetDiscountedPrice(decimal discountedPrice)
    {
        DiscountedPrice = discountedPrice;
        IncrementVersion();
    }

    public void SetStockQuantity(int stockQuantity)
    {
        StockQuantity = stockQuantity;
        IncrementVersion();
    }
    
    private Product() {}
}