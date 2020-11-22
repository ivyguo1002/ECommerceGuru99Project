using Magento.Config;
using Magento.Enums;
using Magento.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magento.Tests
{
    class Task3 : BaseTest
    {
        [Test]
        [TestCase("Sony Xperia", 1000, "* The maximum quantity allowed for purchase is 500.", "SHOPPING CART IS EMPTY")]
        public void ProductAddedToCartShouldNotBeMoreThanTheAvailableAmount(string model, int qty, string itemMsg, string pageTitle)
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);
            var mobilePage = homePage.NavigateToMobilePage();

            var shoppingCartPage = mobilePage.AddToCart(model);
            shoppingCartPage.EditQTY(qty);
            Assert.That(shoppingCartPage.GetItemMsgTxt, Does.Contain(itemMsg));

            shoppingCartPage.CloseFloatingVideo();
            shoppingCartPage.EmptyCart();
            Assert.That(shoppingCartPage.GetPageTitle(), Is.EqualTo(pageTitle));

        }
    }
}
