using Magento.Config;
using Magento.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Magento.Tests
{
    public class Task10 : BaseTest
    {
        [Test]
        public void SalesOrderCanbeExportedToCSV()
        {
            string csvfilePath = @"C:\Users\Administrator\Downloads\orders.csv";
            if (File.Exists(csvfilePath))
            {
                File.Delete(csvfilePath);
            }

            Driver.Navigate().GoToUrl(MagentoConfig.HomePage + MagentoConfig.BackendLoginPage);
            var backendLoginPage = new BackendLoginPage(Driver);
            var adminPage = backendLoginPage.Login(MagentoConfig.username, MagentoConfig.password);
            var salesOrderPage = adminPage.ClosePopupMsg().GoToSalesOrderPage();
            salesOrderPage.ExportToCSV();
            Thread.Sleep(5000);

            //Verify that the csv file should be generated and downloaded
            //Transfer Assert to Wait for seconds
            Assert.That(Wait.Until(driver => File.Exists(csvfilePath)), Is.True);

            //Read the generated csv file and show in console
            using var fr = new StreamReader(csvfilePath);
            while (fr.ReadLine() != null)
            {
                Console.WriteLine(fr.ReadLine());
            }
        }
    }
}
