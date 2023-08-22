using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Events;
using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Types;
using FluentAssertions;

namespace ECommerce.Modules.Orders.UnitTests.Carts.Entities;

public class CartTests
{
    [Fact]
    public void AddItem_ExistingItem_ShouldIncreasesQuantitySuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 20);
        cart.AddItem(product, 2);

        cart.AddItem(product, 3);

        var addedItem = cart.Items.Single();
        addedItem.Quantity.Should().Be(5);
    }

    [Fact]
    public void AddItem_NewItem_ShouldAddsItemToCartSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 20);

        cart.AddItem(product, 2);

        cart.Items.Should().HaveCount(1);
        cart.Items.First().Product.Should().Be(product);
        cart.Items.First().Quantity.Should().Be(2);
    }

    [Fact]
    public void RemoveItem_ExistingItem_ShouldRemovesItemFromCartSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 20);
        cart.AddItem(product, 2);

        cart.RemoveItem(product);

        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_NonexistentItem_ShouldThrowsCartItemNotFoundException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 20);

        cart.Invoking(c => c.RemoveItem(product))
            .Should().Throw<CartItemNotFoundException>();
    }

    [Fact]
    public void Clear_EmptyCart_ShouldAddsCartClearedEventSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));

        cart.Clear();

        cart.Events.Should().ContainSingle(e => e is CartCleared);
    }

    [Fact]
    public void Checkout_EmptyCart_ShouldThrowsCannotCheckoutEmptyCartException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));

        cart.Invoking(c => c.Checkout())
            .Should().Throw<CannotCheckoutEmptyCartException>();
    }

    [Fact]
    public void Checkout_NotEnoughStock_ShouldThrowsNotEnoughProductsInStockException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 3);
        cart.AddItem(product, 5);

        cart.Invoking(c => c.Checkout())
            .Should().Throw<NotEnoughProductsInStockException>();
    }

    [Fact]
    public void Checkout_ValidCart_ShouldAddsCartCheckedOutEventSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        cart.AddItem(product, 3);

        cart.Checkout();

        cart.Events.Should().ContainSingle(e => e is CartCheckedOut);
    }
}