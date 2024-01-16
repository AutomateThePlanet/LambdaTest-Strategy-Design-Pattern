﻿namespace StrategyDesignPattern.Locators;

public class XPathFindStrategy : FindStrategy
{
    public XPathFindStrategy(string value)
        : base(value)
    {
    }

    public override By Convert() => By.XPath(Value);

    public override string ToString() => $"XPath = {Value}";
}
