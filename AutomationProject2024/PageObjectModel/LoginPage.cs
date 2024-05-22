using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProject2024.PageObjectModel
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }
        
        public IWebElement txtEmail => driver.FindElement(By.Id("email"));
        public IWebElement txtRegEmail => driver.FindElement(By.Id("email_create"));
        public IWebElement txtPass => driver.FindElement(By.Id("passwd"));
        public IWebElement btnSignIn => driver.FindElement(By.Id("SubmitLogin"));
        public IWebElement btnSignUp => driver.FindElement(By.Id("SubmitCreate"));

        public string GetAlertText()
        {
            return driver.FindElement(By.XPath("//div[@class='alert alert-danger']//ol//li")).Text;
        }

        public void SignInAccount(string email, string pass)
        {
            txtEmail.SendKeys(email);
            txtPass.SendKeys(pass);
            btnSignIn.Click();
        }

        public void tryRegisterEmail(string email)
        {
            txtRegEmail.SendKeys(email);
            btnSignUp.Click();
        }


        public void RegisterAccount(string email, string pass)
        {
            txtEmail.SendKeys(email);
            txtPass.SendKeys(pass);
            btnSignIn.Click();
        }
    }
}