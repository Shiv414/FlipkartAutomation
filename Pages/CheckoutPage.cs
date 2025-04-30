using FlipkartAutomation.Pages;
using OpenQA.Selenium;

namespace FlipkartTests.Pages
{
    public class CheckoutPage : BasePage
    {
        // Locators
        private By deliveryAddress = By.XPath("//div[contains(text(),'Delivery Address')]");
        private By continueButton = By.XPath("//button/span[text()='CONTINUE']");
        private By paymentOptions = By.XPath("//div[contains(text(),'Payment Options')]");

        public CheckoutPage(IWebDriver driver) : base(driver) { }

        public bool IsDeliveryAddressDisplayed()
        {
            return IsElementPresent(deliveryAddress);
        }

        public void ClickContinue()
        {
            Click(continueButton);
        }

        public bool ArePaymentOptionsDisplayed()
        {
            return IsElementPresent(paymentOptions);
        }
    }
}