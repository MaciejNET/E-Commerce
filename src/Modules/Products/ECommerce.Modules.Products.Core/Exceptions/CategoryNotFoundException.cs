using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Products.Core.Exceptions;

internal sealed class CategoryNotFoundException : ECommerceException
{
    public string Name { get; }

    public CategoryNotFoundException(string name) : base($"Category with ID: '{name}' was not found.")
    {
        Name = name;
    }
}