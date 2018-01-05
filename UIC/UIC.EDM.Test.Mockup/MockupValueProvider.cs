using System;
using System.Globalization;
using System.Linq;
using UIC.Framework.Interfaces.Util;

namespace UIC.EDM.Test.Mockup
{
    internal class MockupValueProvider
    {
        private readonly Random _random;

        public MockupValueProvider() {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        private string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public string GetRandomValueOf(UicDataType type) {
            switch (type) {
                case UicDataType.Unknown:
                    return "Unknown";
                case UicDataType.Integer:
                    int intResult = _random.Next(-100,100);
                    return intResult.ToString(CultureInfo.InvariantCulture);
                case UicDataType.Double:
                    double doubleResult = _random.Next(-100,100) + _random.NextDouble();
                    return doubleResult.ToString("F3", CultureInfo.InvariantCulture);
                case UicDataType.Bool:
                    bool boolResult = ((_random.Next()%2) == 0);
                    return boolResult.ToString(CultureInfo.InvariantCulture);
                case UicDataType.Gps:
                    return "";
                case UicDataType.String:
                    return RandomString(5);
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }
}