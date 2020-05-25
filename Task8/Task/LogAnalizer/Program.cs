using System;
using System.IO;
using LogAnalyzer.Infrastructure;

namespace LogAnalyzer
{
    class Program
    {
        private const string EnterFilePathMessage = "Enter log file path to analyze: ";
        private const string PressEnterToExistMessage = "Press 'Enter' to exit...";

        static void Main(string[] args)
        {
            string logFilePath;
            do
            {
                Console.Write(EnterFilePathMessage);
                logFilePath = Console.ReadLine();
            } while (string.IsNullOrEmpty(logFilePath));

            if (!File.Exists(logFilePath))
            {
                Console.Write($"File {logFilePath} doesn't exist. {PressEnterToExistMessage}");
                Console.ReadLine();
                return;
            }

            var logAnalyzer = new LogFileAnalyzer();
            Console.WriteLine("Report generation started.");
            try
            {
                var reportFilePath = logAnalyzer.Analyze(logFilePath);
                Console.WriteLine(
                    $"Report was generated and located at {reportFilePath}\r\n{PressEnterToExistMessage}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"FATAL ERROR: {e.Message}\r\n{PressEnterToExistMessage}");
            }

            Console.ReadLine();
        }
    }
}