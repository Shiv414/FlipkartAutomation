using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace FlipkartAutomation.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        protected void Click(By locator)
        {
            wait.Until(d => d.FindElement(locator)).Click();
        }

        protected void SendKeys(By locator, string text)
        {
            wait.Until(d => d.FindElement(locator)).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return wait.Until(d => d.FindElement(locator)).Text;
        }

        protected bool IsElementPresent(By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected void WaitForPageLoad()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}