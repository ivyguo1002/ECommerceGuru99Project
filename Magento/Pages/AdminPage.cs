using Magento.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace Magento.Pages
{
    internal class AdminPage
    {
        public IWebDriver Driver { get; set; }
        IWebElement CloseLink => Driver.WaitForElement(By.CssSelector("a[title='close']"));
        IWebElement SalesMenu => Driver.WaitForElement(By.XPath("//a[contains(., 'Sales')]"));
        IWebElement OrdersMenu => Driver.WaitForElement(By.XPath("//a[contains(., 'Orders')]"));

        public AdminPage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        internal AdminPage ClosePopupMsg()
        {
            CloseLink.Click();
            return this;
        }

        internal SalesOrderPage GoToSalesOrderPage()
        {
            var actions = new Actions(Driver);
            actions.MoveToElement(SalesMenu).Perform();
            OrdersMenu.Click();
            return new SalesOrderPage(Driver);
        }
    }
}