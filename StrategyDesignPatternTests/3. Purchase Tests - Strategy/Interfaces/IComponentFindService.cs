// Copyright 2024 Automate The Planet Ltd.
// Author: Anton Angelov
using System.Collections.Generic;
using StrategyDesignPattern.ThirdVersion;
using StrategyDesignPattern.Locators;

namespace StrategyDesignPattern;

public interface IComponentFindService
{
    ComponentAdapter FindById(string id);
    ComponentAdapter FindByXPath(string xpath);
    ComponentAdapter FindByTag(string tag);
    ComponentAdapter FindByClass(string cssClass);
    ComponentAdapter FindByCss(string css);
    ComponentAdapter FindByLinkText(string linkText);
    List<ComponentAdapter> FindAllById(string id);
    List<ComponentAdapter> FindAllByXPath(string xpath);
    List<ComponentAdapter> FindAllByTag(string tag);
    List<ComponentAdapter> FindAllByClass(string cssClass);
    List<ComponentAdapter> FindAllByCss(string css);
    List<ComponentAdapter> FindAllByLinkText(string linkText);

    List<ComponentAdapter> FindAll(FindStrategy findStrategy);
    ComponentAdapter Find(FindStrategy findStrategy);
}
