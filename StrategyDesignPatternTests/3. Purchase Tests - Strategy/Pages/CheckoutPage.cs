using System.Threading;
using StrategyDesignPattern.Models;

namespace StrategyDesignPattern.ThirdVersion;
public class CheckoutPage : WebPage
{
    public CheckoutPage(IDriver driver) 
        : base(driver)
    {
    }

    public IComponent FirstNameInput => Driver.FindById("input-payment-firstname");
    public IComponent LastNameInput => Driver.FindById("input-payment-lastname");
    public IComponent EmailInput => Driver.FindById("input-payment-email");
    public IComponent TelephoneInput => Driver.FindById("input-payment-telephone");
    public IComponent PasswordInput => Driver.FindById("input-payment-password");
    public IComponent ConfirmPasswordInput => Driver.FindById("input-payment-confirm");
    public IComponent CompanyInput => Driver.FindById("input-payment-company");
    public IComponent Address1Input => Driver.FindById("input-payment-address-1");
    public IComponent Address2Input => Driver.FindById("input-payment-address-2");
    public IComponent CityInput => Driver.FindById("input-payment-city");
    public IComponent PostCodeInput => Driver.FindById("input-payment-postcode");
    public IComponent ShippingAddressCountrySelect => Driver.FindById("input-payment-country");
    public IComponent ShippingAddressCountryOption(string country) =>
        ShippingAddressCountrySelect.FindComponent(By.XPath($".//option[contains(text(), '{country}')]"));
    public IComponent BillingAddressRegionSelect => Driver.FindById("input-payment-zone");
    public IComponent BillingAddressRegionOption(string region) =>
        BillingAddressRegionSelect.FindComponent(By.XPath($".//option[contains(text(), '{region}')]"));
    public IComponent TermsAgreeCheckbox => Driver.FindByXPath("//input[@id='input-agree']//following-sibling::label");
    public IComponent ContinueButton => Driver.FindByXPath("//button[@id='button-save']");
    public IComponent TotalPrice => Driver.FindAllByXPath("//td[text()='Total:']/following-sibling::td/strong").Last();

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
        var continueButton = Driver.FindByXPath("//button[@id='button-save']");
        continueButton.Click();
    }
}
