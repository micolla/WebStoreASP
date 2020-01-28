using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetProvider : ILoggerProvider
    {
        private readonly string _ConfigurationFile;

        private readonly ConcurrentDictionary<string, Log4Net> _Loggers = new ConcurrentDictionary<string, Log4Net>();

        public Log4NetProvider(string ConfigurationFile) => _ConfigurationFile = ConfigurationFile;


        public ILogger CreateLogger(string CategoryName)
        {
            return _Loggers.GetOrAdd(CategoryName, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_ConfigurationFile);
                return new Log4Net(category, xml["log4net"]);
            });
        }

        public void Dispose() => _Loggers.Clear();
    }
}
