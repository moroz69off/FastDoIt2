using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDoIt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ResourceWriter RW = new ResourceWriter(@".\FastResources.resx"))
            {// add resources here
                RW.AddResource("isActionGoodResult", false);
            }

            Console.Title = "Fast DO IT";

            GetDriver();


            Actor actor = new Actor("o_0");
                Console.ReadLine();
        }

        private static void GetDriver()
        {
            string path = "https://chromedriver.storage.googleapis.com/87.0.4280.20/chromedriver_win32.zip"; // Quick reference from selenium.dev/documentation/en/webdriver/driver_requirements/
            try
            {
                var dir = Directory.GetCurrentDirectory();
                var files = Directory.GetFiles(dir, "chromedriver.exe");
                if (files[0] != "chromedriver.exe") 
					{
						Console.WriteLine($"Current directory ({dir}) should contains file \"chromedriver.exe\"");
						Console.WriteLine($"We will download from the Internet");
					}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File \"chromedriver.exe\" not found\n * * * \nSystem error message:\n\t{ex.Message}", "File driwer error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                GetChromeDriver(path);
            }
        }

        private static void GetChromeDriver(string path)
        {
            Console.WriteLine("Dounloading driver from https://chromedriver.storage.googleapis.com");
            string folderPath = @"C:\WebDriver\tmp\";
            string fileName = @"ChromeDriver.zip";

            Download();
            void Download()
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    try
                    {
                        client.DownloadFile(new Uri(path), folderPath + fileName);
                        Console.WriteLine($"File {fileName} downloaded to {folderPath}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message+ex.StackTrace);
                    }
                }
            }

            UnZip();
            void UnZip()
            {
                
            }
        }

        class Actor
        {
            public Actor()
            {
                if (true)
                {
                    isGoodResult = true;
                }
            }

            public Actor(string result)
            {
                if (result=="o_0")
                {
                    isGoodResult = true;
                }
            }
            bool isGoodResult { get; set; } = false;
        }
    }
}
