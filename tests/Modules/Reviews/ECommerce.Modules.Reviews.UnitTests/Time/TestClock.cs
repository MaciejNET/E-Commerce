using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Reviews.UnitTests.Time;

public class TestClock : IClock
{
    public DateTime CurrentDate() => new DateTime(2023, 8, 10, 12, 0, 0);
}