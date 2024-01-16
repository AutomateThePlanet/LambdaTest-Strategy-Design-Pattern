using StrategyDesignPattern;
using StrategyDesignPattern.Locators;
using StrategyDesignPattern.ThirdVersion;

namespace ExtensibilityDemos.Extensions;

public static class ElementFindServiceExtensions
{
    public static IComponent CreateByIdContaining(this IComponentFindService findService, string idContaining)
    {
        return findService.Find(new IdContainingFindStrategy(idContaining));
    }
}
