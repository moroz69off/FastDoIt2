using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace FastDoIt
{
    class Program
    {
        private static List<string> links;

        static void Main(string[] args)
        {
            using (ResourceWriter RW = new ResourceWriter(@".\FastResources.resx"))
            {// add resources here
                RW.AddResource("isActionGoodResult", false);
            }

            Console.Title = "Fast DO IT";

            GetDriver();

            GetChromeBro();

            links = GetLinks();

            var store = GoToTheStoreForPedals("");

            var stoResult = store.IsCanceled;
            Console.WriteLine(stoResult);

            FActor actor = new FActor("o_0");

            string result = actor.GetResult("GetResult method (string)");

            Console.WriteLine(result);

            Console.ReadLine();
        }
        private static List<string> GetLinks()
        {

            throw new NotImplementedException();
        }

        async static Task GoToTheStoreForPedals(string link)
        {
            await PushKithButton(link);
            throw new NotImplementedException();

            Task PushKithButton(string model)
            {
                //throw new NotImplementedException();
                return new Task(acton);
            }
            void acton()
            {
                
            }
        }

        private static void GetChromeBro()
        {
            Console.WriteLine("Install Chrome browser,\nbro...");
            //throw new NotImplementedException();
        }

        private static void GetDriver()
        {
            string path = "https://chromedriver.storage.googleapis.com/87.0.4280.20/chromedriver_win32.zip"; // Quick reference from selenium.dev/documentation/en/webdriver/driver_requirements/ ***** lat 87.0.4280.20 - 05.11.2020
            try
            {
                var dir = Directory.GetCurrentDirectory();
                var files = Directory.GetFiles(dir, "chromedriver.exe");

                if (files[0] != "chromedriver.exe" || files.Length == 0) 
					{
						Console.WriteLine($"Current directory ({dir}) should contains file \"chromedriver.exe\"");
						Console.WriteLine($"We will download from the Internet");
					}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                GetChromeDriver(path);
            }
        }

        private static void GetChromeDriver(string path)
        {
            Console.WriteLine("Dounloading driver from https://chromedriver.storage.googleapis.com");
            string folderPath = @"C:\WebDriver\tmp\", fileName = @"ChromeDriver.zip";

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            

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
                        Console.WriteLine(ex.Message + ex.StackTrace);
                    }
                }
            }

            UnZip();
            void UnZip()
            {
                if (!Directory.Exists(@"C:\WebDriver\bin\")) Directory.CreateDirectory(@"C:\WebDriver\bin\");
                if (File.Exists(@"C:\WebDriver\bin\chromedriver.exe")) File.Delete(@"C:\WebDriver\bin\chromedriver.exe");
                
                ZipFile.ExtractToDirectory(folderPath + fileName, @"C:\WebDriver\bin\");
                Console.WriteLine(@"File chromedriver.exe unzip to C:\WebDriver\bin\");
            }
        }
    }
}
