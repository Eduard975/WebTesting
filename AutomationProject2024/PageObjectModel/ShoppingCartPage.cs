using OpenQA.Selenium;
using System.Threading;

namespace AutomationProject2024.PageObjectModel
{
    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnProceedToCheckout => 
            driver.FindElement(By.XPath("//*[@data-role='proceed-to-checkout']"));

        public ShippingAddressPage ProceedToCheckoutPage()
        {
            // Era o reclama deasupra butonului de checkout
            // Folosim JS sa navigam pana in partea de jos a paginii unde butonul nu mai este acoperit
            // alta solutie ar fi fost sa inchidem reclama
            // https://stackoverflow.com/questions/18833064/scroll-the-page-almost-to-the-end-in-selenium
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            BtnProceedToCheckout.Click();

            return new ShippingAddressPage(driver);
        }
    }
}
