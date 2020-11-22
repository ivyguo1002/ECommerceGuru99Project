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
    public class Task2 : BaseTest
    {
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
    }
}
