using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FastDoIt
{
    class ChroBro
    {
        bool isResultOk { get; }
        public ChroBro(string link)
        {
            bool isOk = true;

            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions chromeOptions = new ChromeOptions();
            //chromeOptions.AddWindowType("webview");
            ChromeDriver chromeDriver = new ChromeDriver(driverService, chromeOptions, TimeSpan.FromMilliseconds(3333));
            var driverSessionId = chromeDriver.SessionId;
            Navigate();
            void Navigate()
            {
                chromeDriver.Navigate().GoToUrl(link);
            }
            if (isOk)
            {
                isResultOk = true;
            }

        }
    }
}
