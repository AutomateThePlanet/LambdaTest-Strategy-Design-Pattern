using StrategyDesignPattern.ThirdVersion;

namespace StrategyDesignPattern;

public interface IElementWaitService
{
    void Wait(ComponentAdapter element, WaitStrategy waitStrategy);
}
