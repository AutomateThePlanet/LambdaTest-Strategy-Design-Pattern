﻿namespace StrategyDesignPattern.ThirdVersion;
public class CartPage : WebPage
{
    public CartPage(IDriver driver) 
        : base(driver)
    {
    }

    public ComponentAdapter ViewCartButton => Driver.FindByXPath("//a[normalize-space(.)='View Cart']");
    public ComponentAdapter CheckoutButton => Driver.FindAllByXPath("//a[normalize-space(.)='Checkout']").Last();
    public List<ComponentAdapter> CartItems => Driver.FindAllByCss("div.cart-item");
    public ComponentAdapter TotalPrice => Driver.FindAllByXPath("//td[text()='Total:']/following-sibling::td/strong").Last();

    public void ViewCart()
    {
        ViewCartButton.Click();
    }

    public void Checkout()
    {
        CheckoutButton.Click();
    }

    public void UpdateQuantity(int itemIndex, int quantity)
    {
        var quantityField = CartItems[itemIndex].FindComponent(By.XPath(".//input[@type='number']"));
        //quantityField.Clear();
        quantityField.TypeText(quantity.ToString());
    }

    public void RemoveItem(int itemIndex)
    {
        var removeButton = CartItems[itemIndex].FindComponent(By.XPath(".//button[@title='Remove']"));
        removeButton.Click();
    }

    public void AssertTotalPrice(string expectedPrice)
    {
        Assert.That(TotalPrice.Text, Is.EqualTo(expectedPrice));
    }
}
