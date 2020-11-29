using Magento.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magento.Tests
{
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [SetUp]
        public void Setup()
        {
            switch (MagentoConfig.Browser)
            {
                case "firefox":
                    var profile = new FirefoxProfile();
                    profile.SetPreference("intl.accept_languages", "en,en-US");
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.Profile = profile;
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Driver = new FirefoxDriver(firefoxOptions);
                    break;
                case "chrome":
                    Driver = new ChromeDriver();
                    break;
                default:
                    break;
            }

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
