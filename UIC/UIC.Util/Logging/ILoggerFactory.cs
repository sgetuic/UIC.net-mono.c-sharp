using System;

namespace UIC.Util.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLoggerFor(Type type);
        ILogger GetLoggerFor(string loggername);
    }

    public class NlogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLoggerFor(Type type) {
            return NLogger.CreateLogger(type.Name);
        }

        public ILogger GetLoggerFor(string loggername) {
            return NLogger.CreateLogger(loggername);
        }
    }
}