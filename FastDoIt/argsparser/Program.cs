using System;
using System.ComponentModel.Design.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace argsparser
{
    class Program
    {
        private static string switch_on { get; set; } = "";
        private static int timeOut { get; set; }
        private static int interVal { get; set; }
        private static bool isDebug { get; set; } = false;
        private static IReadOnlyCollection<string> ProfileInfo { get; set; }

        private static void Main(string[] margs)
        {
            if (margs.Length > 0)
            {
                Console.WriteLine("This app startet with parameter/s");
                for (int i = 0; i < margs.Length; i++)
                {
                    switch_on = margs[i];
                    switch (switch_on)
                    {
                        case "-d":
                            Console.WriteLine("Case Debug");
                            isDebug = true;
                            break;
                        case "-t":
                            Console.WriteLine("Case Timeout");
                            timeOut = int.Parse(margs[++i]);
                            break;
                        case "-p":
                            Console.WriteLine("Case Profile");
                            // profile info write with starts and ands " char`s, whitespace char - is a separator of profile info items
                            ProfileInfo = new List<string>(margs[++i].Trim(new char[] { '"' }).Split(new char[] { ' ' }));
                            break;
                        case "-i":
                            Console.WriteLine("Case Interval");
                            interVal= int.Parse(margs[++i]);
                            break;
                        default: Console.WriteLine("Case Default"); break;
                    }
                    Console.WriteLine($"Arg {i} = {margs[i]}");
                }
            }

            Console.WriteLine($"Args length = {margs.Length}");
            Console.WriteLine($"Debug = {isDebug}");
            Console.WriteLine($"Timeout = {timeOut}");
            Console.WriteLine($"Interval = {interVal}");
            Console.WriteLine($"Profile info = {SummaryProfileInfo(ProfileInfo)}");

            Console.ReadLine();
        }

        private static string SummaryProfileInfo(IReadOnlyCollection<string> profileInfo)
        {
            string result = "";
            foreach (var item in ProfileInfo)
            {
                result += item + " ";
            }
            return result;
        }
    }
}
