using ECommerce.Modules.Orders.Domain.Carts.Events;
using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Entities;

public class Cart : AggregateRoot
{
    private readonly List<CartItem> _items = new();
    
    public UserId UserId { get; private set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    internal Cart(AggregateId id, UserId userId) 
        => (Id, UserId) = (id, userId);

    public static Cart Create(AggregateId id, UserId userId)
        => new(id, userId);

    public void AddItem(Product product, int quantity)
    {
        var existingItem = _items.SingleOrDefault(x => x.Product == product);
        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            _items.Add(CartItem.Create(quantity, product));
        }
        IncrementVersion();
    }

    public void RemoveItem(Product product)
    {
        var item = _items.SingleOrDefault(x => x.Product == product);
        if (item is null)
        {
            throw new CartItemNotFoundException(product.Id);
        }

        _items.Remove(item);
        IncrementVersion();
    }
    
    public void Clear()
    {
        _items.Clear();
        AddEvent(new CartCleared(Id, Items));
        IncrementVersion();
    }

    public void Checkout()
    {
        if (Items.Count == 0)
        {
            throw new CannotCheckoutEmptyCartException();
        }

        foreach (var item in Items)
        {
            if (item.Product.StockQuantity - item.Quantity < 0)
            {
                throw new NotEnoughProductsInStockException(item.Product.Id);
            }
        }
        
        AddEvent(new CartCheckedOut(this));
        IncrementVersion();
    }
    
    private Cart() {}
}