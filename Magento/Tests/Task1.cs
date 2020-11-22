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
    public class Task1 : BaseTest
    {
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

        

    }
}