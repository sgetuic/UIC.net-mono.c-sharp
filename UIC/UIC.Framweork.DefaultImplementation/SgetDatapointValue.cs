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

        
        public SgetDatapointValue(string value, DatapointDefinition definition) {
            Value = value;
            Definition = definition;
            Value = VerifiyValue(value);
        }

        public SgetDatapointValue(int value, DatapointDefinition definition)
        {
            Definition = definition;
            if(definition.DataType == UicDataType.Integer)Value = value;
            else if (definition.DataType == UicDataType.Double) Value = (double)value;
            else throw new Exception(definition + " is not of datatype int or double: " + definition.DataType.ToString());
        }

        public SgetDatapointValue(bool value, DatapointDefinition definition)
        {
            if (definition.DataType != UicDataType.Bool) throw new Exception(definition + " is not of datatype bool: " + definition.DataType.ToString());
            Definition = definition;
            Value = value;
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
                    bool result;
                    if (bool.TryParse(value, out result)) {
                        return result;
                    }
                    else if(value=="1") {
                        return true;
                    }
                    else if(value=="0") {
                        return false;
                    }
                    throw new ArgumentException("Could not verify value to bool: " + value);
                case UicDataType.Gps:
                    return value;
                case UicDataType.String:
                    return value;
                default:
                    throw new ArgumentOutOfRangeException(Definition.DataType.ToString());
            }
        }

        public override string ToString()
        {
            return Value + " - " + Definition;
        }
    }
}