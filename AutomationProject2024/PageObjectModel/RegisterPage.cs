using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationProject2024.PageObjectModel
{
    public class RegisterPage
    {
        private IWebDriver driver;

        public RegisterPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement txtFirstname => driver.FindElement(By.Id("customer_firstname"));
        public IWebElement txtLastName => driver.FindElement(By.Id("customer_lastname"));
        public IWebElement txtPass => driver.FindElement(By.Id("passwd"));
        public IWebElement btnSignUP => driver.FindElement(By.Id("submitAccount"));

        public IWebElement btnDays => driver.FindElement(By.Id("days"));
        public IWebElement btnYears => driver.FindElement(By.Id("uniform-years")); 

        public string GetAlertText()
        {
            return driver.FindElement(By.XPath("//p[@class='alert alert-success']")).Text;
        }
        internal RegisterPage FillInformation(string gender, string firstname, string lastname, string password, string birthdate)
        {
            if (gender == "M")
            {
                driver.FindElement(By.Id("id_gender1")).Click();
            }
            else 
            {
                driver.FindElement(By.Id("id_gender2")).Click();
            }

            txtFirstname.SendKeys(firstname);
            txtLastName.SendKeys(lastname);
            txtPass.SendKeys(password);


            string[] date = birthdate.Split(' ');

            btnDays.FindElement(By.XPath($"//option[@value='{date[0]}']")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.XPath($"//*[@id=\"months\"]/option[{date[1]}]")).Click();
            Thread.Sleep(1000);

            btnYears.FindElement(By.XPath($"//option[@value='{date[2]}']")).Click();
            Thread.Sleep(1000);


            btnSignUP.Click();

            return new RegisterPage(driver);
        }
    }
}
