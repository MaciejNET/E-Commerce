using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Returns.Application.Exceptions;

public sealed class NoReturnableProductsException : ECommerceException
{
    public string Sku { get; }

    public NoReturnableProductsException(string sku) : base($"No returnable products found for the SKU: {sku}.")
    {
        Sku = sku;
    }
}