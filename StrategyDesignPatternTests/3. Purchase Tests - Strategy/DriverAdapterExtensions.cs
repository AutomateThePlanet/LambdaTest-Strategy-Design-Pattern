using StrategyDesignPattern;
using StrategyDesignPattern.Locators;
using StrategyDesignPattern.ThirdVersion;

namespace ExtensibilityDemos.Extensions;

public static class DriverAdapterExtensions
{
    public static ComponentAdapter CreateByIdContaining(this DriverAdapter driverAdapter, string idContaining)
    {
        return driverAdapter.Find(new IdContainingFindStrategy(idContaining));
    }
}
