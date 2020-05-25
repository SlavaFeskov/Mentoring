using log4net;

namespace MvcMusicStore.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog _log4NetLogger;

        public Logger()
        {
            _log4NetLogger = LogManager.GetLogger("Logger");
        }

        public void Error(string message)
        {
            if (_log4NetLogger.IsErrorEnabled)
            {
                _log4NetLogger.Error(message);
            }
        }

        public void Info(string message)
        {
            if (_log4NetLogger.IsInfoEnabled)
            {
                _log4NetLogger.Info(message);
            }
        }

        public void Debug(string message)
        {
            if (_log4NetLogger.IsDebugEnabled)
            {
                _log4NetLogger.Debug(message);
            }
        }

        public void Warn(string message)
        {
            if (_log4NetLogger.IsWarnEnabled)
            {
                _log4NetLogger.Warn(message);
            }
        }
    }
}