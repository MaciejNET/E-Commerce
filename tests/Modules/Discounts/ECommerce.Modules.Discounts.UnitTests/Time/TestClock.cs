using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Discounts.UnitTests.Time;

public class TestClock : IClock
{
    public DateTime CurrentDate() => new DateTime(2023, 8, 15);
}