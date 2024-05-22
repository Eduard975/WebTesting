using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
