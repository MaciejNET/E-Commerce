using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Products.Core.Exceptions;

internal sealed class CategoryAlreadyExistsException : ECommerceException
{
    public string Name { get; }

    public CategoryAlreadyExistsException(string name) : base($"Category with name: {name} already exists.")
    {
        Name = name;
    }
}