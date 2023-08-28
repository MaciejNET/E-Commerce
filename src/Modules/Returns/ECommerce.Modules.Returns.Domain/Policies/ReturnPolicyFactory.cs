using ECommerce.Modules.Returns.Domain.Entities;

namespace ECommerce.Modules.Returns.Domain.Policies;

public class ReturnPolicyFactory : IReturnPolicyFactory
{
    public IReturnPolicy Get(ReturnType type)
        => type switch
        {
            ReturnType.Return => new ReturnTypePolicy(),
            ReturnType.Guarantee => new GuaranteeTypePolicy(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}