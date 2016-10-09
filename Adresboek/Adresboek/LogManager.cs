using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Adresboek
{
    class LogManager
    {
        // create a log file on every test-run (in the folder "/Logs")
        // a seperate log file for prime-paths and oracles are created.
        static private string FileName = String.Format("{0:yyyy-M-d HH.mm.ss}", DateTime.Now) + ".txt";
        static private string FileNamePrime = String.Format("{0:yyyy-M-d HH.mm.ss}", DateTime.Now) + "-prime.txt";

        // write to the log file
        static public void Write(string s)
        {
            LogManager.Write(s, false);
        }

        // write to the log-file
        // when the second parameter is true, thann it will logs to the pime-path-test log file
        static public void Write(string s, bool primePathTest)
        {
            string filename = primePathTest ? LogManager.FileNamePrime : LogManager.FileName;
            StreamWriter writer = new StreamWriter("../../../Logs/"+filename, true);
            writer.WriteLine(s);
            writer.Close();
        }

        // oracles in the code itself will be logged to the oracles.txt log file
        static public void Oracle(string s, bool b)
        {
            StreamWriter writer = new StreamWriter("../../../Logs/" + "Oracles.txt", true);
            writer.Write(b + " " + s);
            writer.WriteLine("");

            writer.Close();
        }
    }
}
