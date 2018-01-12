using System;

namespace UIC.Util.Extensions {
    public static class UintExtensions {
        public static string ToBinaryString(this uint value, int length=16) {
            return Convert.ToString(value, 2).PadLeft(length, '0');
        }

    }
}
