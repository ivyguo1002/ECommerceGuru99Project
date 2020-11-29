using Magento.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Magento.Pages
{
    internal class SalesOrderPage
    {
        public IWebDriver Driver { get; set; }
        SelectElement ExportToDropdownMenu => new SelectElement(Driver.WaitForElement(By.Id("sales_order_grid_export")));
        IWebElement ExportBtn => Driver.WaitForElement(By.CssSelector("button[title='Export']"));
        public SalesOrderPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal void ExportToCSV()
        {
            ExportToDropdownMenu.SelectByText("CSV");
            ExportBtn.Click();
        }
    }
}