using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ECommerce.Modules.Products.Core.DTO;

internal class CategoryDto
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }
}