using Magento.Config;
using Magento.Enums;
using Magento.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
        public void MobileListPageCanBeSortedByName()
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);
            Assert.That(homePage.PageTitle, Does.Contain("THIS IS DEMO SITE"));

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

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}