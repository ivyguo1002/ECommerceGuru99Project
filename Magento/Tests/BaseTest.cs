using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magento.Tests
{
    public class BaseTest
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

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
