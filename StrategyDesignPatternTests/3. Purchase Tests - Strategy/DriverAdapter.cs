using ExtensibilityDemos;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using StrategyDesignPattern.Locators;
using WebDriverManager.DriverConfigs.Impl;

namespace StrategyDesignPattern.ThirdVersion;

public class DriverAdapter : IDriver
{
    private IWebDriver _webDriver;

    private WebDriverWait _webDriverWait;
    private ComponentFindService _componentFindService;

    public string Url => _webDriver.Url;

    public void Start(Browser browser)
    {
        switch (browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                _webDriver = new ChromeDriver();
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                _webDriver = new FirefoxDriver();
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                _webDriver = new EdgeDriver();
                break;
            case Browser.Safari:
                _webDriver = new SafariDriver();
                break;
            case Browser.InternetExplorer:
                new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerConfig());
                _webDriver = new InternetExplorerDriver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
        }

        _webDriver.Manage().Window.Maximize();
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _componentFindService = new ComponentFindService(_webDriver, _webDriver);
    }

    public void Quit()
    {
        _webDriver.Quit();
    }

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
    }

    //public IComponent FindComponent(By locator)
    //{
    //    IComponent nativeWebComponent = 
    //        _webDriverWait.Until(ExpectedConditions.ComponentExists(locator));
    //    IComponent IComponent = new ComponentAdapter(_webDriver, nativeWebComponent, locator);

    //    ScrollIntoView(Component);
    //    ////HighlightComponent(Component);
    //    return Component;
    //}

    //public List<IComponent> FindComponents(By locator)
    //{
    //    ReadOnlyCollection<IWebComponent> nativeWebComponents = 
    //        _webDriverWait.Until(ExpectedConditions.PresenceOfAllComponentsLocatedBy(locator));
    //    var Components = new List<IComponent>();
    //    foreach (var nativeWebComponent in nativeWebComponents)
    //    {
    //        IComponent IComponent = new ComponentAdapter(_webDriver, nativeWebComponent, locator);
    //        Components.Add(Component);
    //    }

    //    //if (Components.Any())
    //    //{
    //    //    ScrollIntoView(Components.Last());
    //    //    HighlightComponent(Components.Last());
    //    //}

    //    return Components;
    //}

    public void Refresh()
    {
        _webDriver.Navigate().Refresh();
    }

    public bool ComponentExists(IComponent component)
    {
        try
        {
            _webDriver.FindElement(component.By);

            return true;
        }
        catch
        {
            // The IComponent was not found
            return false;
        }
    }

    public void DeleteAllCookies()
    {
        _webDriver.Manage().Cookies.DeleteAllCookies();
    }

    public void ExecuteScript(string script, params object[] args)
    {
        ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, args);
    }

    public void WaitForAjax()
    {
        _webDriverWait.Until(driver =>
        {
            var script = "return window.jQuery != undefined && jQuery.active == 0";
            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
        });
    }

    public List<IComponent> FindAllByClass(string cssClass)
    {
        return FindAll(new ClassFindStrategy(cssClass));
    }

    public List<IComponent> FindAllById(string id)
    {
        return FindAll(new IdFindStrategy(id));
    }

    public List<IComponent> FindAllByTag(string tag)
    {
        return FindAll(new TagFindStrategy(tag));
    }

    public List<IComponent> FindAllByXPath(string xpath)
    {
        return FindAll(new XPathFindStrategy(xpath));
    }

    public List<IComponent> FindAllByCss(string css)
    {
        return FindAll(new CssFindStrategy(css));
    }

    public List<IComponent> FindAllByLinkText(string linkText)
    {
        return FindAll(new LinkTextFindStrategy(linkText));
    }

    public IComponent FindByCss(string css)
    {
        return Find(new CssFindStrategy(css));
    }

    public IComponent FindByLinkText(string linkText)
    {
        return Find(new LinkTextFindStrategy(linkText));
    }

    public IComponent FindByClass(string cssClass)
    {
        return Find(new ClassFindStrategy(cssClass));
    }

    public IComponent FindById(string id)
    {
        return Find(new IdFindStrategy(id));
    }

    public IComponent FindByTag(string tag)
    {
        return Find(new TagFindStrategy(tag));
    }

    public IComponent FindByXPath(string xpath)
    {
        return Find(new XPathFindStrategy(xpath));
    }

    public IComponent Find(FindStrategy findStrategy)
    {
        var nativeComponent = _componentFindService.Find(findStrategy);
        return new ComponentAdapter(_webDriver, nativeComponent, findStrategy.Convert());
    }

    public List<IComponent> FindAll(FindStrategy findStrategy)
    {
        var nativeComponents = _componentFindService.FindAll(findStrategy);
        var resultComponents = new List<IComponent>();
        foreach (var nativeComponent in nativeComponents)
        {
            resultComponents.Add(new ComponentAdapter(_webDriver, nativeComponent, findStrategy.Convert()));
        }

        return resultComponents;
    }

    public void Wait(IComponent Component, WaitStrategy waitStrategy)
    {
        waitStrategy?.WaitUntil(_webDriver, _webDriver, Component.By);
    }

    private void ScrollIntoView(IComponent Component)
    {
        ExecuteScript("arguments[0].scrollIntoView(true);", Component.WrappedElement);
    }
}
