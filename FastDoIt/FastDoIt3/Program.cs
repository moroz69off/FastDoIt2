using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FastDoIt3
{
    class Program
    {
        public static string sessionId { get; private set; }

        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference("Browser", LogLevel.Off);
            options.SetLoggingPreference("Driver" , LogLevel.Off);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://kith.com");
            sessionId = driver.CurrentWindowHandle;
            Console.WriteLine(sessionId);
            Console.ReadLine();
        }
    }
}
