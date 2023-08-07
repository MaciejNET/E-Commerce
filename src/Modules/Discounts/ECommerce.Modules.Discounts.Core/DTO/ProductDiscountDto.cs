using System.ComponentModel.DataAnnotations;

namespace ECommerce.Modules.Discounts.Core.DTO;

public class ProductDiscountDto
{
    public Guid Id { get; set; }
    
    [Range(1, int.MaxValue)]
    public decimal NewPrice { get; set; }
    public Guid ProductId { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}