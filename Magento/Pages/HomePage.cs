using Magento.Config;
using Magento.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Magento.Pages
{
    internal class HomePage
    {
        public IWebDriver Driver { get; set; }
        public const string Title = "Home page";
        public string PageTitle => Driver.WaitForElement(By.CssSelector(".page-title>h2")).Text;
        private IWebElement MobileMenu => Driver.WaitForElement(By.XPath("//a[contains(., 'Mobile')]"));

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
            Assert.That(Driver.Title, Is.EqualTo(Title));
        }

        internal MobilePage NavigateToMobilePage()
        {
            MobileMenu.Click();
            return new MobilePage(Driver);
        }

    }
}