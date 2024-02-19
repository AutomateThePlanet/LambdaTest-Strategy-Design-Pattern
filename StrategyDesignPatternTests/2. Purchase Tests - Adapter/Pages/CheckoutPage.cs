using StrategyDesignPattern.Models;

namespace StrategyDesignPattern.SecondVersion;
public class CheckoutPage : WebPage
{
    public CheckoutPage(IDriver driver) 
        : base(driver)
    {
    }

    public ComponentAdapter FirstNameInput => Driver.FindComponent(By.Id("input-payment-firstname"));
    public ComponentAdapter LastNameInput => Driver.FindComponent(By.Id("input-payment-lastname"));
    public ComponentAdapter EmailInput => Driver.FindComponent(By.Id("input-payment-email"));
    public ComponentAdapter TelephoneInput => Driver.FindComponent(By.Id("input-payment-telephone"));
    public ComponentAdapter PasswordInput => Driver.FindComponent(By.Id("input-payment-password"));
    public ComponentAdapter ConfirmPasswordInput => Driver.FindComponent(By.Id("input-payment-confirm"));
    public ComponentAdapter CompanyInput => Driver.FindComponent(By.Id("input-payment-company"));
    public ComponentAdapter Address1Input => Driver.FindComponent(By.Id("input-payment-address-1"));
    public ComponentAdapter Address2Input => Driver.FindComponent(By.Id("input-payment-address-2"));
    public ComponentAdapter CityInput => Driver.FindComponent(By.Id("input-payment-city"));
    public ComponentAdapter PostCodeInput => Driver.FindComponent(By.Id("input-payment-postcode"));
    public ComponentAdapter ShippingAddressCountrySelect => Driver.FindComponent(By.Id("input-payment-country"));
    public ComponentAdapter ShippingAddressCountryOption(string country) =>
        ShippingAddressCountrySelect.FindComponent(By.XPath($".//option[contains(text(), '{country}')]"));
    public ComponentAdapter BillingAddressRegionSelect => Driver.FindComponent(By.Id("input-payment-zone"));
    public ComponentAdapter BillingAddressRegionOption(string region) =>
        BillingAddressRegionSelect.FindComponent(By.XPath($".//option[contains(text(), '{region}')]"));
    public ComponentAdapter TermsAgreeCheckbox => Driver.FindComponent(By.XPath("//input[@id='input-agree']//following-sibling::label"));
    public ComponentAdapter ContinueButton => Driver.FindComponent(By.XPath("//button[@id='button-save']"));
    public ComponentAdapter TotalPrice => Driver.FindComponents(By.XPath("//td[text()='Total:']/following-sibling::td/strong")).Last();

    public void FillUserDetails(UserDetails userDetails)
    {
        FirstNameInput.TypeText(userDetails.FirstName);
        LastNameInput.TypeText(userDetails.LastName);
        EmailInput.TypeText(userDetails.Email);
        TelephoneInput.TypeText(userDetails.Telephone);
        PasswordInput.TypeText(userDetails.Password);
        ConfirmPasswordInput.TypeText(userDetails.ConfirmPassword);
    }

    public void FillBillingAddress(BillingAddress billingAddress)
    {
        CompanyInput.TypeText(billingAddress.Company);
        Address1Input.TypeText(billingAddress.Address1);
        Address2Input.TypeText(billingAddress.Address2);
        CityInput.TypeText(billingAddress.City);
        PostCodeInput.TypeText(billingAddress.PostCode);
        ShippingAddressCountrySelect.Click();
        ShippingAddressCountryOption(billingAddress.Country).Click();
        //Thread.Sleep(1000);
        Driver.WaitForAjax();
        BillingAddressRegionSelect.Click();
        BillingAddressRegionOption(billingAddress.Region).Click();
    }

    public void AgreeToTerms()
    {
        Driver.WaitForAjax();
        // TODO: Move to Driver as addition to FindComponent as decoratr
        // TODO: Add addtional decorator for highlighting element
        //((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", TermsAgreeCheckbox);
        TermsAgreeCheckbox.Click(true);
    }

    public void ClickContinue()
    {
        ContinueButton.Click();
    }

    public void AssertTotalPrice(string expectedPrice)
    {
        Assert.That(TotalPrice.Text, Is.EqualTo(expectedPrice));
    }

    public void CompleteCheckout()
    {
        var continueButton = Driver.FindComponent(By.XPath("//button[@id='button-save']"));
        continueButton.Click();
    }
}
