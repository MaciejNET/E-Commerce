using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Orders.Entities;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Shared.Enums;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using FluentAssertions;

namespace ECommerce.Modules.Orders.UnitTests.Orders.Entities;

public class OrderTests
{
    [Fact]
    public void CreateFromCheckout_WithDiscount_AppliesDiscountedPrices()
    {
        var now = DateTime.Now;
        var userId = new UserId(Guid.NewGuid());
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10M, 5);
        var cart = Cart.Create(new AggregateId(), userId);
        cart.AddItem(product, 3);
        var checkoutCart = new CheckoutCart(cart);
        var discount = Discount.Create(new AggregateId(), "CODE123", 10, new[] {product});
        var shipment = new Shipment("City", "Street", 123, "Receiver");
        var paymentMethod = PaymentMethod.Cashless;
        checkoutCart.SetShipment(shipment);
        checkoutCart.SetPayment(paymentMethod);
        checkoutCart.ApplyDiscount(discount);

        var order = Order.CreateFromCheckout(checkoutCart, now);

        order.Lines.Should().ContainSingle();
        var orderLine = order.Lines.Single();
        orderLine.UnitPrice.Should().Be(9); // 10 * (10% discount)
    }

    [Fact]
    public void CreateFromCheckout_NoDiscount_UsesStandardPrices()
    {
        var now = DateTime.Now;
        var userId = new UserId(Guid.NewGuid());
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        var cart = Cart.Create(new AggregateId(), userId);
        cart.AddItem(product, 3);
        var checkoutCart = new CheckoutCart(cart);
        var shipment = new Shipment("City", "Street", 123, "Receiver");
        var paymentMethod = PaymentMethod.Cashless;
        checkoutCart.SetShipment(shipment);
        checkoutCart.SetPayment(paymentMethod);

        var order = Order.CreateFromCheckout(checkoutCart, now);

        order.Lines.Should().ContainSingle();
        var orderLine = order.Lines.Single();
        orderLine.UnitPrice.Should().Be(10);
    }

    [Fact]
    public void StartProcessing_ValidOrder_ChangesStatusToInProgress()
    {
        var order = CreateOrder();

        order.StartProcessing();

        order.Status.Should().Be(OrderStatus.InProgress);
    }

    [Fact]
    public void StartProcessing_InvalidStatus_ThrowsInvalidOrderStatusChangeException()
    {
        var order = CreateOrder();
        order.StartProcessing();
        order.Send();

        order.Invoking(o => o.StartProcessing())
            .Should().Throw<InvalidOrderStatusChangeException>();
    }

    [Fact]
    public void Send_ValidOrder_ChangesStatusToSent()
    {
        var order = CreateOrder();
        order.StartProcessing();

        order.Send();

        order.Status.Should().Be(OrderStatus.Sent);
    }

    [Fact]
    public void Send_InvalidStatus_ThrowsInvalidOrderStatusChangeException()
    {
        var order = CreateOrder();

        order.Invoking(o => o.Send())
            .Should().Throw<InvalidOrderStatusChangeException>();
    }

    [Fact]
    public void Complete_ValidOrder_ChangesStatusToCompleted()
    {
        var order = CreateOrder();
        order.StartProcessing();
        order.Send();
        var now = DateTime.Now;

        order.Complete(now);

        order.Status.Should().Be(OrderStatus.Completed);
        order.CompletionDate.Should().Be(now);
    }

    [Fact]
    public void Complete_InvalidStatus_ThrowsInvalidOrderStatusChangeException()
    {
        var order = CreateOrder();
        order.StartProcessing();

        order.Invoking(o => o.Complete(DateTime.Now))
            .Should().Throw<InvalidOrderStatusChangeException>();
    }

    [Fact]
    public void Cancel_CompletedOrder_ThrowsInvalidOrderStatusChangeException()
    {
        var order = CreateOrder();
        order.StartProcessing();
        order.Send();
        order.Complete(DateTime.Now);

        order.Invoking(o => o.Cancel())
            .Should().Throw<InvalidOrderStatusChangeException>();
    }

    [Fact]
    public void Cancel_SentOrder_ThrowsInvalidOrderStatusChangeException()
    {
        var order = CreateOrder();
        order.StartProcessing();
        order.Send();

        order.Invoking(o => o.Cancel())
            .Should().Throw<InvalidOrderStatusChangeException>();
    }

    [Fact]
    public void Cancel_ValidOrder_ChangesStatusToCanceled()
    {
        var order = CreateOrder();
        order.StartProcessing();

        order.Cancel();

        order.Status.Should().Be(OrderStatus.Canceled);
    }

    private Order CreateOrder()
    {
        var userId = new UserId(Guid.NewGuid());
        var product = new Product(new AggregateId(), "Product 1", "SKU123", 10, 5);
        var cart = new Cart(new AggregateId(), userId);
        cart.AddItem(product, 3);
        var checkoutCart = new CheckoutCart(cart);
        var shipment = new Shipment("City", "Street", 123, "Receiver");
        var paymentMethod = PaymentMethod.Cashless;
        checkoutCart.SetShipment(shipment);
        checkoutCart.SetPayment(paymentMethod);
        var now = DateTime.Now;

        return Order.CreateFromCheckout(checkoutCart, now);
    }
}