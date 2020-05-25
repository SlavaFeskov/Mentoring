namespace LogAnalyzer.Infrastructure.Abstractions
{
    public interface ILogAnalyzer
    {
        /// <summary>
        /// Parses log file located by <see cref="logFilePath"/> path and generated report.
        /// </summary>
        /// <param name="logFilePath">Log file path.</param>
        /// <returns>Report file path.</returns>
        string Analyze(string logFilePath);
    }
}