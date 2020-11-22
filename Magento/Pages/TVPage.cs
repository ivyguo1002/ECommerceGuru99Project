using Magento.Utils;
using OpenQA.Selenium;
using System;

namespace Magento.Pages
{
    internal class TVPage
    {
        public IWebDriver Driver { get; set; }
        public TVPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal WishListPage AddToWishList(string product)
        {
            Driver.WaitForElement(By.XPath("//h2[contains(., '" + product + "')]//following-sibling::div//a[@class='link-wishlist']")).Click();
            return new WishListPage(Driver);
        }
    }
}