using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllSign {
    internal class Program {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            string pfx = "";
            string signDir = "";

            // check args
            if(args.Length == 1) {
                // sign current directory's files
                pfx = args[0];
                signDir = Environment.CurrentDirectory;
            }
            else if(args.Length == 2) {
                // sign the files in the directory specified
                pfx = args[1];
                signDir = args[2];
            }
            else {
                // invalid/no args
                Help.ShowUsage();
                return;
            }

            Sign(pfx, signDir, "*.dll"); // sign all DLL's
            Sign(pfx, signDir, "*.exe"); // sign all EXE's
        }

        /// <summary>
        /// Sign all files in a directory of a specific type
        /// </summary>
        /// <param name="pfx"></param>
        /// <param name="signDir"></param>
        /// <param name="type"></param>
        public static void Sign(string pfx, string signDir, string type) {
            foreach (var file in Directory.EnumerateFiles(signDir, type, SearchOption.AllDirectories)) {
                try {
                    using (var proc = new Process()) {
                        proc.StartInfo.FileName = "signtool.exe";
                        proc.StartInfo.Arguments = $"sign /v /f {pfx} /t http://timestamp.digicert.com /fd SHA256 {file}";

                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.RedirectStandardOutput = true;

                        proc.Start();

                        do {
                            Thread.Sleep(50);
                            Console.Out.Write(proc.StandardOutput.ReadToEnd());
                        }
                        while (!proc.HasExited);

                        Console.Out.Write(proc.StandardOutput.ReadToEnd());
                    }
                }
                catch(Exception ex) {
                    Console.WriteLine(Debugger.IsAttached ? ex.ToString() : ex.Message);
                }
            }
        }
    }
}
