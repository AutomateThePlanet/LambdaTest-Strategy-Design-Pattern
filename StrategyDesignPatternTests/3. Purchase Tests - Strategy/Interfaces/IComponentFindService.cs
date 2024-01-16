// Copyright 2024 Automate The Planet Ltd.
// Author: Anton Angelov
using System.Collections.Generic;
using StrategyDesignPattern.ThirdVersion;
using StrategyDesignPattern.Locators;

namespace StrategyDesignPattern;

public interface IComponentFindService
{
    IComponent FindById(string id);
    IComponent FindByXPath(string xpath);
    IComponent FindByTag(string tag);
    IComponent FindByClass(string cssClass);
    IComponent FindByCss(string css);
    IComponent FindByLinkText(string linkText);
    List<IComponent> FindAllById(string id);
    List<IComponent> FindAllByXPath(string xpath);
    List<IComponent> FindAllByTag(string tag);
    List<IComponent> FindAllByClass(string cssClass);
    List<IComponent> FindAllByCss(string css);
    List<IComponent> FindAllByLinkText(string linkText);

    List<IComponent> FindAll(FindStrategy findStrategy);
    IComponent Find(FindStrategy findStrategy);
}
