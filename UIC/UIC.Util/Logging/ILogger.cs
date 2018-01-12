using System;

namespace UIC.Util.Logging {
    public interface ILogger {
        void Debug(string message, params object[] args);
        void Information(string message, params object[] args);
        void Warning(string message, params object[] args);
        void Error(string message, params object[] args);
        void Error(Exception exception, string message = null, params object[] args);
        void Fatal(string message, params object[] args);
        void Fatal(Exception exception, string message = null, params object[] args);
    }
}