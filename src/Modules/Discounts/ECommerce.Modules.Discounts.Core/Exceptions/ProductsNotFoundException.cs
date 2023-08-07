using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class ProductsNotFoundException : ECommerceException
{
    public ProductsNotFoundException() : base("Some products were not found.")
    {
    }
}