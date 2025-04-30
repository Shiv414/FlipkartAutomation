using FlipkartAutomation.Pages;
using OpenQA.Selenium;

namespace FlipkartTests.Pages
{
    public class CommonElements : BasePage
    {
        // Locators for common elements
        private By flipkartLogo = By.XPath("//img[@title='Flipkart']");
        private By footerLinks = By.XPath("//div[contains(@class,'_2Brcj4')]//a");

        public CommonElements(IWebDriver driver) : base(driver) { }

        public bool IsLogoDisplayed()
        {
            return IsElementPresent(flipkartLogo);
        }

        public int GetFooterLinkCount()
        {
            return driver.FindElements(footerLinks).Count;
        }
    }
}
