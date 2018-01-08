using System;
using System.Globalization;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation

{
    public class SgetDatapointValue : DatapointValue
    {
        public DatapointDefinition Definition { get; }
        public object Value { get; }

        public SgetDatapointValue(object value, DatapointDefinition definition) {

            Definition = definition;
            Value = VerifiyValue(value);
        }

        public SgetDatapointValue(string value, DatapointDefinition definition) {

            Value = value;
            Definition = definition;
            Value = VerifiyValue(value);
        }

        private object VerifiyValue(string value)
        {

            switch (Definition.DataType)
            {
                case UicDataType.Unknown:
                    return value;
                case UicDataType.Integer:
                    return int.Parse(value, CultureInfo.InvariantCulture);
                case UicDataType.Double:
                    return double.Parse(value, CultureInfo.InvariantCulture);
                case UicDataType.Bool:
                    return bool.Parse(value);
                case UicDataType.Gps:
                    return value;
                case UicDataType.String:
                    return value;
                default:
                    throw new ArgumentOutOfRangeException(Definition.DataType.ToString());
            }
        }

        private object VerifiyValue(object value)
        {

            switch (Definition.DataType)
            {
                case UicDataType.Unknown:
                    return value;
                case UicDataType.Integer:
                    return (int)value;
                case UicDataType.Double:
                    return (double)value;
                case UicDataType.Bool:
                    return (bool)value;
                case UicDataType.Gps:
                    return value;
                case UicDataType.String:
                    return value.ToString();
                default:
                    throw new ArgumentOutOfRangeException(Definition.DataType.ToString());
            }
        }
        
    }
}