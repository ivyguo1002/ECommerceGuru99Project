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
    public class Tests
    {
        public IWebDriver Driver { get; set; }

        [SetUp]
        public void Setup()
        {
            var profile = new FirefoxProfile();
            profile.SetPreference("intl.accept_languages", "en,en-US");
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.Profile = profile;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Driver = new FirefoxDriver(firefoxOptions);
        }

        [Test]
        [TestCase("THIS IS DEMO SITE")]
        public void MobileListPageCanBeSortedByName(string title)
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);
            Assert.That(homePage.PageTitle, Does.Contain(title));

            var mobilePage = homePage.NavigateToMobilePage();
            mobilePage.SortBy(mobilePage.TopSortBYMenu,SortMenu.Name);
            Assert.That(mobilePage.ProductNames, Is.Ordered);
        }

        [Test]
        [TestCase("Sony Xperia", "$100.00")]
        public void ProductCostInListPageAndDetailsPageShouldBeEqual(string model, string price)
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);
            var mobilePage = homePage.NavigateToMobilePage();

            string priceInListPage = mobilePage.GetPhoneModelPrice(model);
            Assert.That(priceInListPage, Is.EqualTo(price));

            var detailsPage = mobilePage.NavigateToDetailsPage(model);
            string priceInDetailsPage = detailsPage.GetPhoneModelPrice();
            Assert.That(priceInDetailsPage, Is.EqualTo(priceInListPage));
        }


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

        [Test]
        [TestCase("Sony Xperia", "IPhone")]
        public void UserShouldBeAbleToCompareProducts(string model1, string model2)
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);
            var mobilePage = homePage.NavigateToMobilePage();

            mobilePage.AddToCompare(model1)
                .AddToCompare(model2)
                .Compare();
            string currentWindow = Driver.CurrentWindowHandle;
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(driver => driver.WindowHandles.Count == 2);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The Compare Window didn't pop up");
            }

            foreach (var windowHandle in Driver.WindowHandles)
            {
                if (!windowHandle.Equals(currentWindow))
                {
                    Driver.SwitchTo().Window(windowHandle);
                }
            }

            var comparePage = new ComparePage(Driver);
            Assert.That(comparePage.GetProductsList(), Is.EqualTo(new List<string>() { "SONY XPERIA", "IPHONE" }));
            comparePage.CloseWindow();
            try
            {
                wait.Until(driver => driver.WindowHandles.Count == 1);
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail("The Compare Window didn't close");
            }


        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}