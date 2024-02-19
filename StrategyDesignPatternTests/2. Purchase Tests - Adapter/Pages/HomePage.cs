namespace StrategyDesignPattern.SecondVersion;
public class HomePage : WebPage
{
    public HomePage(IDriver driver) 
        : base(driver)
    {
    }

    public ComponentAdapter SearchInput => Driver.FindComponent(By.XPath("//input[@name='search']"));

    public void SearchProduct(string searchText)
    {
        //SearchInput.Clear();
        SearchInput.TypeText(searchText);
        //SearchInput.SendKeys(Keys.Enter);
    }
}
