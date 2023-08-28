using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Returns.Domain.Policies;

public interface IReturnPolicy
{
    void Return(Return @return, IClock clock);
}