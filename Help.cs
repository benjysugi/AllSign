using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSign {
    internal class Help {
        /// <summary>
        /// Write usage to the console
        /// </summary>
        public static void ShowUsage() {
            Console.WriteLine("USAGE: sign.exe <PFX>");
            Console.WriteLine("USAGE: sign.exe <PFX> <DIRECTORY>");
            Console.WriteLine();
        }
    }
}
