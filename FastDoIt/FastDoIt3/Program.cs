using System;
using System.Collections.Generic;
using System.IO;
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
            Console.Title = "FastDoIt";
            List<string> profileInfoList = GetProfile("profiles.csv", 1); // 1 - fisrt profile in profiles list (for multiprofiles work)
            List<string> links = GetLinks();

            ChromeDriverService chromeDriverService = AddService();

            ChromeOptions options = InitOptions();

            object preference = null;
            options.AddLocalStatePreference("", preference);

            IWebDriver driver = new ChromeDriver(chromeDriverService, options);
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(links[1]);
            //https://kith.com/products/nkda1469-200
            //https://kith.com/collections/stone-island-ghost-capsule/products/simo7315s02f6-v0070


            driver.Quit();
            Console.ReadLine();
        }

        private static List<string> GetLinks()
        {
            return new List<string>(File.ReadAllLines("links").ToList());
        }

        private static ChromeOptions InitOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference("Browser", LogLevel.All); // temp
            options.SetLoggingPreference("Driver", LogLevel.All); // temp
                                                                  //options.AddArguments("--disable-infobars"); // temp
            return options;
        }
        /// <summary>
        /// Get profile information from "profiles.csv" file by index
        /// </summary>
        /// <param name="profilesPath">string, path to "profiles.csv" file</param>
        /// <param name="profileNum">index of profile</param>
        /// <returns>List<string> profile data</returns>
        private static List<string> GetProfile(string profilesPath, int profileNum)
        {
            List<List<string>> profilesData = new List<List<string>>();
            List<string> vs = new List<string>(File.ReadAllLines(profilesPath));
            for (int i = 0; i < vs.Count; i++)
            {
                profilesData.Add(vs[i].Split(new char[] { ',' }).ToList());
            }
            return new List<string>(profilesData[profileNum]);
        }

        private static ChromeDriverService AddService()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            return chromeDriverService;
        }
    }
}
