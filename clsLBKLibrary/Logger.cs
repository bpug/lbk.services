using System;
using System.IO;
namespace clsLBKLibrary
{
    public class Logger
    {
        private static int _ALL = 100;
        private static int _INFO = 75;
        private static int _ERROR = 50;
        private static int _OFF = 0;
        private static String logDirectory = System.Configuration.ConfigurationSettings.AppSettings["logDirectory"].ToString();
        public static int ALL
        {
            get { return _ALL; }
        }
        public static int ERROR
        {
            get { return _ERROR; }
        }
        public static int INFO
        {
            get { return _INFO; }
        }
        public static int OFF
        {
            get { return _OFF; }
        }
        public static void Append(String message, int level)
        {
                int logLevel = _OFF;
                String strLogLevel = System.Configuration.ConfigurationSettings.AppSettings["logLevel"].ToString();
                switch (strLogLevel)
                {
                    case "ALL":
                        logLevel = _ALL;
                        break;
                    case "ERROR":
                        logLevel = _ERROR;
                        break;
                    case "INFO":
                        logLevel = _INFO;
                        break;
                    default:
                        logLevel = _OFF;
                        break;
                }
                if (logLevel >= level)
                {
                    DateTime dt = DateTime.Now;
                    String filePath = logDirectory + dt.ToString("yyyyMMdd") + ".log";
                    if (!File.Exists(filePath))
                    {
                        FileStream fs = File.Create(filePath);
                        fs.Close();
                    }
                    try
                    {
                        StreamWriter sw = File.AppendText(filePath);
                        sw.WriteLine(dt.ToString("hh:mm:ss") + "|" + message);
                        sw.Flush();
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToString());
                    }
                }
         


        }
    }
}