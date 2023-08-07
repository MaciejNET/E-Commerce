using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Modules.Discounts.Core.Entities;

internal class ProductDiscount
{
    public Guid Id { get; set; }
    public decimal NewPrice { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}