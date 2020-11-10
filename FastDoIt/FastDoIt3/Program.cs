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
        //https://kith.com/collections/stone-island-ghost-capsule/products/simo7315s02f6-v0070
        //backToShop glDefaultBtn
        static void Main(string[] args)
        {
            ChromeDriverService chromeDriverService = AddService();

            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference("Browser", LogLevel.All);
            options.SetLoggingPreference("Driver", LogLevel.All);
            options.AddArguments("--disable-infobars");
            
            object preference = null;
            options.AddLocalStatePreference("", preference);

            IWebDriver driver = new ChromeDriver(chromeDriverService, options);
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            sessionId = driver.CurrentWindowHandle;
            Console.WriteLine($"sessionId: {sessionId}");

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://kith.com/collections/stone-island-ghost-capsule/products/simo7315s02f6-v0070");



            driver.Quit();
            Console.ReadLine();
        }

        private static ChromeDriverService AddService()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            processId = chromeDriverService.ProcessId;
            Console.WriteLine($"Process Id: {processId}");
            return chromeDriverService;
        }
    }
}
