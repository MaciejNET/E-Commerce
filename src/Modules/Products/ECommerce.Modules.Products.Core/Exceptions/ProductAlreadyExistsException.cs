using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Products.Core.Exceptions;

internal sealed class ProductAlreadyExistsException : ECommerceException
{
    public string Sku { get; }

    public ProductAlreadyExistsException(string sku) : base($"Product with SKU: {sku} already exists.")
    {
        Sku = sku;
    }
}