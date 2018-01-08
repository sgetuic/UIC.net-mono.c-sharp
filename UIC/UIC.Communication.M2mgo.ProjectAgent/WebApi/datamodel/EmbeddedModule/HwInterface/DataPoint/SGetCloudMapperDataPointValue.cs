using System;
using System.Globalization;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint {
    public class SGetCloudMapperDataPointValue {
        private readonly SGetCloudMapperDataPointDefinition _definition;
        private readonly object _value;
        private readonly DateTime _ocurredTimeStamp;

        public SGetCloudMapperDataPointValue(SGetCloudMapperDataPointDefinition definition, string value, DateTime ocurredTimeStamp) {
            if (definition == null)
                throw new ArgumentNullException("definition");
            if (value == null)
                throw new ArgumentNullException("value");


            _definition = definition;
            _ocurredTimeStamp = ocurredTimeStamp;

            _value = VerifiyValue(value);
        }

        private object VerifiyValue(string value)
        {

            switch (_definition.DataType)
            {
                case SGetCloudMapperDataType.Unknown:
                    return value;
                case SGetCloudMapperDataType.Int:
                    return int.Parse(value, CultureInfo.InvariantCulture);
                case SGetCloudMapperDataType.Double:
                    return double.Parse(value, CultureInfo.InvariantCulture);
                case SGetCloudMapperDataType.Boolean:
                    return bool.Parse(value);
                case SGetCloudMapperDataType.Long:
                    return long.Parse(value, CultureInfo.InvariantCulture);
                case SGetCloudMapperDataType.Gps:
                    return value;
                case SGetCloudMapperDataType.String:
                    return value;
                default:
                    throw new ArgumentOutOfRangeException(_definition.DataType.ToString());
            }


        }

        public double GetValueAsDouble() {
            if (_definition.DataType != SGetCloudMapperDataType.Double) {
                throw new Exception("Datatype is not a double: " + _definition.DataType);
            }
            return (double)_value;
        }

        public int GetValueAsinteger() {
            if (_definition.DataType != SGetCloudMapperDataType.Int) {
                throw new Exception("Datatype is not a int: " + _definition.DataType);
            }
            return (int)_value;
        }

        public DateTime GetTimeStamp() {
            return _ocurredTimeStamp;
        }

        public SGetCloudMapperDataPointDefinition GetDefinition() {
            return _definition;
        }

        public string GetValueAsString() {
            switch (_definition.DataType) {
                case SGetCloudMapperDataType.Int:
                    return GetValueAsinteger().ToString(CultureInfo.InvariantCulture);
                case SGetCloudMapperDataType.Double:
                    return GetValueAsDouble().ToString(CultureInfo.InvariantCulture);
                case SGetCloudMapperDataType.Unknown:
                case SGetCloudMapperDataType.Boolean:
                case SGetCloudMapperDataType.Gps:
                case SGetCloudMapperDataType.String:
                    return _value.ToString();
                default:
                    throw new ArgumentOutOfRangeException(_definition.DataType.ToString());
            }
        }

        public object GetValue() {
            return _value;
        }
    }
}