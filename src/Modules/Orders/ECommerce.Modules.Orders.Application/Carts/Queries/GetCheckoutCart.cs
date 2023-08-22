using ECommerce.Modules.Orders.Application.Carts.DTO;
using ECommerce.Shared.Abstractions.Queries;

namespace ECommerce.Modules.Orders.Application.Carts.Queries;

public class GetCheckoutCart : IQuery<CheckoutCartDto>
{
    public Guid UserId { get; set; }
}