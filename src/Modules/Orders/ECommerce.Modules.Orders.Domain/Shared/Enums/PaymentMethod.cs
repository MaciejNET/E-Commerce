using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Shared.Entities;

public enum PaymentMethod
{
    Cash,
    Cashless,
    Loan
}