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
            try
            {
                var dir = Directory.GetCurrentDirectory();
                var files = Directory.GetFiles(dir, "webdriver.exe");
                if (files[0] != "webdriver.exe") 
					{
						string path = "https://chromedriver.storage.googleapis.com/87.0.4280.20/chromedriver_win32.zip"; // Quick reference from https://www.selenium.dev/documentation/en/webdriver/driver_requirements/
						Console.WriteLine($"Current directory ({dir}) should contains file \"webdriver.exe\"");
						Console.WriteLine($"We will download from the Internet");
						GetDriver(path);
					}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File \"webdriver.exe\" not found\n * * * \nSystem error message:\n\t{ex.Message}", "File driwer error", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
