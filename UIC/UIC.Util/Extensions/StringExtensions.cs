using System;

namespace UIC.Util.Extensions {
    public static class StringExtensions {
        public static bool IsNullOrEmpty(this string text) {
            return String.IsNullOrEmpty(text);
        }

        public static bool IsNullOrWhiteSpace(this string text) {
            return string.IsNullOrWhiteSpace(text);
        }
    }
}
