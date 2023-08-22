using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Events;
using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Shared.Entities;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;
using ECommerce.Modules.Orders.UnitTests.Shared.Time;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Time;
using FluentAssertions;

namespace ECommerce.Modules.Orders.UnitTests.Carts.Entities;

public class CheckoutCartTests
{
    [Fact]
    public void SetPayment_SetsPaymentMethod()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var checkoutCart = new CheckoutCart(cart);

        checkoutCart.SetPayment(PaymentMethod.Cashless);

        checkoutCart.Payment.Should().Be(PaymentMethod.Cashless);
    }

    [Fact]
    public void SetShipment_SetsShipment()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var checkoutCart = new CheckoutCart(cart);
        var shipment = new Shipment("City", "Street", 123, "Receiver");

        checkoutCart.SetShipment(shipment);

        checkoutCart.Shipment.Should().BeEquivalentTo(shipment);
    }

    [Fact]
    public void ApplyDiscount_ProductDiscountWithValidProducts_AppliesDiscount()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        cart.AddItem(product, 3);

        var discount = Discount.Create(new AggregateId(), "CODE123", 10, new[] {product});

        var checkoutCart = new CheckoutCart(cart);
        checkoutCart.ApplyDiscount(discount);

        checkoutCart.Discount.Should().BeEquivalentTo(discount);
    }

    [Fact]
    public void ApplyDiscount_ProductDiscountWithInvalidProducts_ThrowsDiscountApplicationException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        cart.AddItem(product, 3);

        var discount = Discount.Create(new AggregateId(), "CODE123", 10,
            new[] {new Product(new AggregateId(), "Product 2", "SKU456", 10, 5)});

        var checkoutCart = new CheckoutCart(cart);

        checkoutCart.Invoking(c => c.ApplyDiscount(discount))
            .Should().Throw<DiscountApplicationException>();
    }

    [Fact]
    public void PlaceOrder_NotEnoughStock_ThrowsNotEnoughProductsInStockException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        cart.AddItem(product, 10);

        var checkoutCart = new CheckoutCart(cart);

        checkoutCart.Invoking(c => c.PlaceOrder(_clock))
            .Should().Throw<NotEnoughProductsInStockException>();
    }

    [Fact]
    public void PlaceOrder_ValidCart_AddsEvents()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()));
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        cart.AddItem(product, 3);

        var checkoutCart = new CheckoutCart(cart);

        checkoutCart.PlaceOrder(_clock);

        checkoutCart.Events.Should().ContainSingle(e => e is OrderPlaced);
        checkoutCart.Events.Should().ContainSingle(e => e is CartCheckoutProcessed);
        checkoutCart.Events.Should().ContainSingle(e => e is ProductBought);
    }

    private readonly IClock _clock;

    public CheckoutCartTests()
    {
        _clock = new TestClock();
    }
}