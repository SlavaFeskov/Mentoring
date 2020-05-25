using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LogAnalyzer.Infrastructure.Abstractions;
using MSUtil;

namespace LogAnalyzer.Infrastructure
{
    public class LogFileAnalyzer : ILogAnalyzer
    {
        private string GetReportFileNamePath(string identifier) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Reports\\Report_{identifier}.txt");

        public string Analyze(string logFilePath)
        {
            if (!logFilePath.EndsWith(".xml"))
            {
                throw new ArgumentException("Only XML log files are supported.");
            }

            var tempFilePath = "";

            try
            {
                tempFilePath = FormatXmlFile(logFilePath);

                var logQuery = new LogQueryClassClass();
                var eachLevelCountResult =
                    logQuery.Execute($"SELECT Level, COUNT(*) FROM {tempFilePath} GROUP BY Level");
                var warnMessages = logQuery.Execute($"SELECT Message FROM {tempFilePath} WHERE Level = 'WARN'");

                var sb = new StringBuilder();
                while (!eachLevelCountResult.atEnd())
                {
                    var record = eachLevelCountResult.getRecord();
                    sb.AppendLine($"Level-Count: {record.getValue(0)}-{record.getValue(1)}");
                    eachLevelCountResult.moveNext();
                }

                sb.AppendLine();

                while (!warnMessages.atEnd())
                {
                    var record = warnMessages.getRecord();
                    sb.AppendLine($"Warn Message: {record.getValue(0)}");
                    warnMessages.moveNext();
                }

                var reportFilePath = GetReportFileNamePath(Path.GetFileNameWithoutExtension(logFilePath));
                var reportsDir = Path.GetDirectoryName(reportFilePath);
                if (!Directory.Exists(reportsDir))
                {
                    Directory.CreateDirectory(reportsDir);
                }

                File.WriteAllText(reportFilePath, sb.ToString());
                return reportFilePath;
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }

        private string FormatXmlFile(string logFilePath)
        {
            var fileText = File.ReadAllText(logFilePath);
            var xmlText = $"<root>{fileText}</root>";
            var tempFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp.xml");
            File.WriteAllText(tempFilePath, xmlText);
            return tempFilePath;
        }
    }
}