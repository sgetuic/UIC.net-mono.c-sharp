using System;
using System.IO;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Util {
    public class ConfigurationJsonFileHandler {
        private readonly string _filePath;
        private readonly ISerializer _serializer;
        private readonly ILogger _logger;
        private readonly FileInfo _backupPath;

        public ConfigurationJsonFileHandler(string filePath, ISerializer serializer, ILogger logger) {
            var pathSeparator = Path.DirectorySeparatorChar;
            logger.Information("DirectorySeparatorChar: " + pathSeparator);
            if (pathSeparator == '/') {
                _filePath = filePath.Replace("\\", "/");
            } else {
                _filePath = filePath.Replace("/", "\\");
            }
            logger.Information("_filePath: " + _filePath);
            string[] strings = _filePath.Split('.');
            if(strings.Length < 2)throw new Exception("config file must have an extension: " + _filePath);
            strings[strings.Length - 2] = strings[strings.Length - 2] + "_bak";
            _backupPath = new FileInfo(String.Join(".", strings));
            _serializer = serializer;
            _logger = logger;
        }

        
        public T Load<T>()
        {
            _logger.Information("Load Config from {0}", _filePath);
            if (IsConfigFileExisting())
            {
                string json = File.ReadAllText(_filePath);
                return _serializer.Deserialize<T>(json);
            }
            throw new IOException("File does not Exist: " + _filePath);
        }

        public bool IsConfigFileExisting()
        {
            bool exists = File.Exists(_filePath);
            _logger.Information("{0} exists: {1}", _filePath, exists);
            return exists;
        }

        public void Backup(object config)
        {
            Backup(_serializer.Serialize(config, true));
        }

        public void Backup(string json)
        {
            _logger.Information("back up to {0}", _backupPath.FullName);
            if (_backupPath.Exists) {
                _backupPath.Delete();
            }
            using (var sr = _backupPath.CreateText())
            {
                sr.Write(json);
            }
        }
    }
}
