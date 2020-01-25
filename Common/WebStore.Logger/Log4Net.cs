using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4Net : ILogger
    {
        private readonly ILog _log;

        public Log4Net(string CategoryName, XmlElement Configuration)
        {
            var loggerRepository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            _log = LogManager.GetLogger(loggerRepository.Name, CategoryName);
            log4net.Config.XmlConfigurator.Configure(loggerRepository, Configuration);
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case LogLevel.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is null) throw new ArgumentNullException(nameof(formatter));

            if (!IsEnabled(logLevel)) return;

            var logMessage = formatter(state, exception);
            if (string.IsNullOrEmpty(logMessage) && exception is null) return;

            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(logMessage);
                    break;
                case LogLevel.Information:
                    _log.Info(logMessage);
                    break;
                case LogLevel.Warning:
                    _log.Warn(logMessage);
                    break;
                case LogLevel.Error:
                    _log.Error(logMessage,exception);
                    break;
                case LogLevel.Critical:
                    _log.Fatal(logMessage,exception);
                    break;
                case LogLevel.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }

        }
        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
