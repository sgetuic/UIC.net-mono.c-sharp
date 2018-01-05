using System;
using System.Diagnostics;
using System.Linq;
using NLog;
using UIC.Util.Extensions;

namespace UIC.Util.Logging {
    public class NLogger : ILogger {
        public static ILogger CreateLogger(Type declaringType)
        {
            return new NLogger(declaringType.PrettyName());
        }

        public static ILogger CreateLogger(string loggername)
        {
            return new NLogger(loggername);
        }


        private readonly Logger NLogLogger;

        private NLogger(string name) {
            NLogLogger = LogManager.GetLogger(name);
        }

        private static string Format(string message, params object[] args) {
            if (message.IsNullOrEmpty())
                return String.Empty;
            
            return args.Any()
                ? String.Format(message, args) : message;
        }

        [Conditional("DEBUG")]
        private void DebugInternal(string message, params object[] args) {
            NLogLogger.Debug(Format(message, args));
        }

        public void Debug(string message, params object[] args) {
            DebugInternal(message, args);
        }


        public void Information(string message, params object[] args) {
            NLogLogger.Info(Format(message, args));
        }


        public void Warning(string message, params object[] args) {
            NLogLogger.Warn(Format(message, args));
        }


        public void Error(string message, params object[] args) {
            NLogLogger.Error(Format(message, args));
        }

        public void Error(Exception exception, string message, params object[] args) {
            NLogLogger.Error(exception, exception.Message + " - " + Format(message, args).IfNot(String.IsNullOrWhiteSpace));
        }


        public void Fatal(string message, params object[] args) {
            NLogLogger.Fatal(Format(message, args));
        }

        public void Fatal(Exception exception, string message, params object[] args) {
            NLogLogger.Fatal(exception, exception.Message + " - " + Format(message, args).IfNot(String.IsNullOrWhiteSpace));
        }
    }
}
