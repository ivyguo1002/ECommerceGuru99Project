using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magento.Utils
{
    public static class WebDriverExtensionMethods
    {
        public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeOutInSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds));
                return wait.Until(driver => driver.FindElement(by));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Find Element Failed By locator {by} in {timeOutInSeconds}");
            }

        }

        public static IList<IWebElement> WaitForElements(this IWebDriver driver, By by, int timeOutInSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds));
                return wait.Until(driver => driver.FindElements(by));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Find Elements Failed By locator {by} in {timeOutInSeconds}");
            }

        }
    }
}
