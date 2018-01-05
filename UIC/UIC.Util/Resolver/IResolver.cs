using System;
using System.Collections.Generic;
using UIC.Util.Logging;

namespace UIC.Util.Resolver {
    public interface IResolver {
        T Get<T>();
        T Get<T>(string name);

        IEnumerable<T> GetAll<T>();

        ILogger GetLoggerFor(Type declaringType);
        ILogger GetLoggerFor(string name);

    }
}
