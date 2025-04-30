using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;

namespace FlipkartTests.Utilities
{
    public class BrowserUtility
    {
        private IWebDriver? driver;

        public IWebDriver InitBrowser(string browserName)
        {
            try
            {
                switch (browserName.ToLower())
                {
                    case "chrome":
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--start-maximized"); // Launch browser maximized
                        chromeOptions.AddArgument("--disable-headless"); // Ensure headless mode is disabled
                        driver = new ChromeDriver(chromeOptions);
                        break;

                    case "firefox":
                        var firefoxOptions = new FirefoxOptions();
                        driver = new FirefoxDriver(firefoxOptions);
                        driver.Manage().Window.Maximize(); // Maximize window
                        break;

                    case "edge":
                        var edgeOptions = new EdgeOptions();
                        driver = new EdgeDriver(edgeOptions);
                        driver.Manage().Window.Maximize(); // Maximize window
                        break;

                    default:
                        throw new ArgumentException($"Unsupported browser: {browserName}");
                }

                return driver;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize browser: {ex.Message}", ex);
            }
        }

        public void NavigateToUrl(string url)
        {
            if (driver == null)
            {
                throw new InvalidOperationException("Driver is not initialized. Call InitBrowser first.");
            }

            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to navigate to URL '{url}': {ex.Message}", ex);
            }
        }

        public void CloseBrowser()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}
