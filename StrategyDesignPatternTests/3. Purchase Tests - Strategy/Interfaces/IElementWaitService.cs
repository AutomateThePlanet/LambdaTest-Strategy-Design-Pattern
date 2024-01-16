using StrategyDesignPattern.ThirdVersion;

namespace StrategyDesignPattern;

public interface IElementWaitService
{
    void Wait(IComponent element, WaitStrategy waitStrategy);
}
