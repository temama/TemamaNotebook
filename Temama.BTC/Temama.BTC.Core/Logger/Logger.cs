using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temama.BTC.Core.Logger
{
    public static class Logger
    {
        public enum LogSeverity
        {
            Spam,
            Info,
            Warning,
            Error,
            Critical
        }

        private static string _fileName = @"Logs\BTC.Core.log";

        public static void Init(string executableName)
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");
            _fileName = string.Format("Logs\\{0}_{1}.log", executableName, DateTime.Now.ToString("yyyyMMdd_HHmmSS"));
        }

        public static void SpamWrite(string message)
        {
            WriteLine(LogSeverity.Spam, message);
        }

        public static void InfoWrite(string message)
        {
            WriteLine(LogSeverity.Info, message);
        }

        public static void WarningWrite(string message)
        {
            WriteLine(LogSeverity.Warning, message);
        }

        public static void ErrorWrite(string message)
        {
            WriteLine(LogSeverity.Error, message);
        }

        public static void CriticalWrite(string message)
        {
            WriteLine(LogSeverity.Critical, message);
        }
        public static void WriteLine(LogSeverity severity, string message)
        {
            try
            {
                File.AppendAllText(_fileName,
                                   string.Format("{0}\t{1}\t{2}\r\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                                                 GetSeverityRepresentation(severity), message));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to write log in log file {0} due to exception: {1}", _fileName, ex.Message);
                Console.WriteLine("{0}\t{1}\t{2}\r\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                                  GetSeverityRepresentation(severity), message);
            }
        }

        private static string GetSeverityRepresentation(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Spam:
                    return "SPAM";
                case LogSeverity.Info:
                    return "INFO";
                case LogSeverity.Warning:
                    return "WARNING";
                case LogSeverity.Error:
                    return "ERROR";
                case LogSeverity.Critical:
                    return "CRITICAL";
                default:
                    return "UNKNOWN";
            }
        }
    }
}
