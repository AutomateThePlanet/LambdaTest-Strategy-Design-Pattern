namespace StrategyDesignPattern.SecondVersion;

public interface IDriver
{
    public string Url { get; }
    public void Start(Browser browser);
    public void Refresh();
    public void Quit();
    public void GoToUrl(string url);
    public ComponentAdapter FindComponent(By locator);
    public List<ComponentAdapter> FindComponents(By locator);

    public bool ComponentExists(ComponentAdapter component);
    public void ExecuteScript(string script, params object[] args);
    public void DeleteAllCookies();
    public void WaitForAjax();
}
