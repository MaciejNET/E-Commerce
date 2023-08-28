using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Returns.Domain.Policies;

public class GuaranteeTypePolicy : IReturnPolicy
{
    public void Return(Return @return, IClock clock)
    {
        if (@return.Order.CompletionDate!.Value.Date <= clock.CurrentDate().Date)
        {
            @return.ChangeStatus(ReturnStatus.Accepted);
            return;
        }
        
        @return.ChangeStatus(ReturnStatus.SendToManualCheck);
    }
}