using System;

namespace UIC.EDM.EApi.BoardInformation.Util
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return String.IsNullOrEmpty(text);
        }

        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
    }
}