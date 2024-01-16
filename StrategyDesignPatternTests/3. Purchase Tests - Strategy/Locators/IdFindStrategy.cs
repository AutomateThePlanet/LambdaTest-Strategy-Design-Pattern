﻿namespace StrategyDesignPattern.Locators;

public class IdFindStrategy : FindStrategy
{
    public IdFindStrategy(string value)
        : base(value)
    {
    }

    public override By Convert()
    {
        return By.Id(Value);
    }
}
