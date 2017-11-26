using System;

namespace UIC.EDM.EApi.BoardInformation.Util
{
    public static class EnumExgtension
    {
        public static TEnum ToEnum<TEnum>(this byte value) where TEnum : struct
        {
            return ((int)value).ToEnum<TEnum>();
        }

        public static TEnum ToEnum<TEnum>(this int value) where TEnum : struct
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)((object)value);
            }
            throw new Exception("Could not parse " + value + " into Enum " + typeof(TEnum));
        }

        public static TEnum ToEnum<TEnum>(this uint value) where TEnum : struct
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)((object)value);
            }
            throw new Exception("Could not parse " + value + " into Enum " + typeof(TEnum));
        }

        public static TEnum ToEnum<TEnum>(this short shortValue) where TEnum : struct
        {
            int value = shortValue;
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)((object)value);
            }
            throw new Exception("Could not parse " + value + " into Enum " + typeof(TEnum));
        }

        public static TEnum ToEnum<TEnum>(this string stringValue, bool ignoreCase = false) where TEnum : struct
        {
            return stringValue.ToEnum<TEnum>(default(TEnum), ignoreCase);
        }
        public static TEnum ToEnum<TEnum>(this string stringValue, TEnum defaultValue, bool ignoreCase = false) where TEnum : struct
        {
            if (stringValue.IsNullOrEmpty())
                return defaultValue;
            TEnum result;
            if (!Enum.TryParse(stringValue, ignoreCase, out result))
            {
                throw new Exception("Could not parse " + stringValue + " into Enum " + typeof(TEnum));
            }
            return result;
        }
    }
}
