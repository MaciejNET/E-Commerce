namespace ECommerce.Modules.Discounts.Core.Entities;

internal class Product
{
    public Guid Id { get; set; }
    public ICollection<DiscountCode> DiscountCodes { get; set; }
}