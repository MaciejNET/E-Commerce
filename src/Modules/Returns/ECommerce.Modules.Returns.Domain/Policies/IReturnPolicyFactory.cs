using ECommerce.Modules.Returns.Domain.Entities;

namespace ECommerce.Modules.Returns.Domain.Policies;

public interface IReturnPolicyFactory
{
    IReturnPolicy Get(ReturnType type);
}