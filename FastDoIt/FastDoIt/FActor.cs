using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDoIt
{
    class FActor
    {
        public FActor(string link) => SnapUp(link);

        private void SnapUp(string link)
        {
            string result = link; // todo

            Console.WriteLine($"\"FActot\" unit created\noutput: {result}");
        }

        public string GetResult(string str)
        {
            string result = str;

            return result;
        }
    }
}
