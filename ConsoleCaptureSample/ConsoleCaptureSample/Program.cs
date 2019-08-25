using System;
using System.IO;
using ConsoleOutputLib;

namespace ConsoleCaptureSample
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Redirecting console output");
            Console.WriteLine("Press any key to close");

            ConsoleDataOut.Start();

            // Attempt to open output file.
            using (var writer = new StreamWriter("console_out.txt"))
            {
                // Redirect standard output from the console to the output file.
                Console.SetOut(writer);

                Console.ReadKey();
            }

            // Recover the standard output stream so that a 
            // completion message can be displayed.
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine("Console out will be captured to console_out.txt"); 
        }
    }
}