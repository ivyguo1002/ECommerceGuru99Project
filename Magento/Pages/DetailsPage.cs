using Magento.Utils;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Magento.Pages
{
    internal class DetailsPage
    {
        private IWebDriver Driver { get; set; }
        private IWebElement Price => Driver.WaitForElement(By.CssSelector("#product-price-1"));
        public DetailsPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal string GetPhoneModelPrice()
        {
            return Price.Text;
        }
    }
}