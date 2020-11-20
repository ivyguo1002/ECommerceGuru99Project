using Magento.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Magento.Pages
{
    internal class ComparePage
    {
        private IWebDriver Driver { get; set; }
        private IList<IWebElement> Products => Driver.WaitForElements(By.CssSelector("h2[class='product-name']"));
        private IWebElement CloseBtn => Driver.WaitForElement(By.CssSelector("button[title='Close Window']"));

        public ComparePage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal List<string> GetProductsList()
        {
            var productNames = new List<string>();
            foreach (var product in Products)
            {
                productNames.Add(product.Text);
            }
            return productNames;
        }

        internal void CloseWindow()
        {
            CloseBtn.Click();
        }
    }

}