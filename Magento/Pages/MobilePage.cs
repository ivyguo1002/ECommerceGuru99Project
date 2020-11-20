using Magento.Enums;
using Magento.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Magento.Pages
{
    internal class MobilePage
    {
        private IWebDriver Driver { get; set; }
        public const string Title = "Mobile";
        internal SelectElement TopSortBYMenu => new SelectElement(Driver.WaitForElements(By.CssSelector("select[title='Sort By']"))[0]);
        internal SelectElement BottomSortBYMenu => new SelectElement(Driver.WaitForElements(By.CssSelector("select[title='Sort By']"))[1]);
        private IList<IWebElement> MobileProductNames => Driver.WaitForElements(By.CssSelector(".product-name"));
        private IWebElement CompareBtn => Driver.WaitForElement(By.CssSelector("button[title='Compare']"));

        public MobilePage(IWebDriver driver)
        {
            Driver = driver;
            Assert.That(Driver.Title, Is.EqualTo(Title));
        }

        internal void SortBy(SelectElement SortBy,SortMenu menu)
        {
            SortBy.SelectByText(menu.ToString());
        }

        internal List<string> ProductNames
        {
            get
            {
                var nameList = new List<string>();
                foreach (var productName in MobileProductNames)
                {
                    nameList.Add(productName.Text);
                }
                return nameList;
            }
        }

        internal string GetPhoneModelPrice(string model)
        {
             return Driver.WaitForElement(By.XPath("//h2[@class='product-name'][contains(., '" + model + "')]//following-sibling::div[@class='price-box']")).Text;
        }

        internal DetailsPage NavigateToDetailsPage(string model)
        {
            Driver.WaitForElement(By.CssSelector("a[title='" + model + "']")).Click();
            return new DetailsPage(Driver);
        }

        internal ShoppingCartPage AddToCart(string model)
        {
            Driver.WaitForElement(By.XPath("//h2[@class='product-name'][contains(., '" + model + "')]//following-sibling::div/button[contains(@class, 'btn-cart')]")).Click();
            return new ShoppingCartPage(Driver);
        }

        internal MobilePage AddToCompare(string model)
        {
            Driver.WaitForElement(By.XPath("//h2[@class='product-name'][contains(., '" + model + "')]//following-sibling::div/ul//li/a[@class='link-compare']")).Click();
            return this;
        }

        internal void Compare()
        {
            CompareBtn.Click();
        }
    }
}