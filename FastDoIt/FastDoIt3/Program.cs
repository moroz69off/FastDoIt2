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
        public static int processId { get; private set; }

        //backToShop glDefaultBtn
        //backToShop glDefaultBtn
        static void Main(string[] args)
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            processId = chromeDriverService.ProcessId;
            Console.WriteLine($"Process Id: {processId}");

            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference("Browser", LogLevel.All);
            options.SetLoggingPreference("Driver" , LogLevel.All);
            options.AddArgument("--disable-infobars");

            IWebDriver driver = new ChromeDriver(chromeDriverService, options);
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://yumor.xyz");
            sessionId = driver.CurrentWindowHandle;
            Console.WriteLine($"sessionId: {sessionId}");
            Console.ReadLine();
        }
    }
}
