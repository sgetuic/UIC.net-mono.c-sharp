using System;
using System.Collections.Generic;
using UIC.EDM.EApi.BoardInformation.EApi.BoardInformation;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdmCapabilityProvider
    {

        private readonly Dictionary<Guid, BoardInformationValueId> _boardValueDic = new Dictionary<Guid, BoardInformationValueId>();
        private readonly Dictionary<Guid, BoardInformationStringId> _boardÍnfoStringDic = new Dictionary<Guid, BoardInformationStringId>();

        public EdmCapability EdmCapability { get; }

        public EapiBoardInformationEdmCapabilityProvider(Edmldentifier Identifier) {
            List<CommandDefinition> commandDefinitions = GetCommands();
            List<AttribtueDefinition> attribtueDefinitions = GetAttributeDefinitions();
            List<DatapointDefinition> datapointDefinitions = GetDatapointDefinitions();
            EdmCapability = new EapiBoardInformationEdmCapability(Identifier, commandDefinitions, attribtueDefinitions, datapointDefinitions);
        }

        internal bool TryGet(Guid id, out BoardInformationStringId stringId)
        {
            return _boardÍnfoStringDic.TryGetValue(id, out stringId);
        }

        internal bool TryGet(Guid datapointId, out BoardInformationValueId boardInformationValueId)
        {
            return _boardValueDic.TryGetValue(datapointId, out boardInformationValueId);
        }

        private List<CommandDefinition> GetCommands()
        {
            return new List<CommandDefinition> {
                new SgetCommandDefinition("Read Board Information", "read", UicDataType.String, String.Empty, null, new string[0])
            };
        }

        private List<AttribtueDefinition> GetAttributeDefinitions() {

            List<AttribtueDefinition> list = new List<AttribtueDefinition>();
            foreach (BoardInformationStringId item in Enum.GetValues(typeof(BoardInformationStringId)))
            {
                AttribtueDefinition attrDefinition = GetAttributeDefinitionOf(item);
                _boardÍnfoStringDic.Add(attrDefinition.Id, item);
                list.Add(attrDefinition);
            }

            foreach (BoardInformationValueId item in Enum.GetValues(typeof(BoardInformationValueId)))
            {
                AttribtueDefinition attrDefinition = GetAttributeDefinitionOf(item);
                if (attrDefinition != null)
                {
                    _boardValueDic.Add(attrDefinition.Id, item);
                    list.Add(attrDefinition);
                }
            }
            return list;
        }

        private List<DatapointDefinition> GetDatapointDefinitions()
        {
            List<DatapointDefinition> list = new List<DatapointDefinition>();
            foreach (BoardInformationValueId item in Enum.GetValues(typeof(BoardInformationValueId)))
            {
                SgetDatapointDefinition datapointDefinition = GetDatapointDefinitionOf(item);
                if (datapointDefinition != null) {
                    _boardValueDic.Add(datapointDefinition.Id, item);
                    list.Add(datapointDefinition);
                }
            }
            return list;
        }


        private SgetDatapointDefinition GetDatapointDefinitionOf(BoardInformationValueId item) {
            switch (item) {
                case BoardInformationValueId.EAPI_ID_GET_EAPI_SPEC_VERSION:
                    return null;
                case BoardInformationValueId.EAPI_ID_BOARD_BOOT_COUNTER_VAL:
                    return new SgetDatapointDefinition(new Guid("0D516431-537D-49E4-88F7-6FD0ED39A142"), UicDataType.Integer, "BOOT_COUNTER_VAL", "Boot Cpunter", string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_RUNNING_TIME_METER_VAL:
                    return new SgetDatapointDefinition(new Guid("A7464FB7-1BDD-45AB-9F57-5E591EBC829E"), UicDataType.Integer, "RUNNING_TIME_METER_VAL", "Running Time", string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_PNPID_VAL:
                    return null;
                case BoardInformationValueId.EAPI_ID_BOARD_PLATFORM_REV_VAL:
                    return null;
                case BoardInformationValueId.EAPI_ID_BOARD_DRIVER_VERSION_VAL:
                    return null;
                case BoardInformationValueId.EAPI_ID_BOARD_LIB_VERSION_VAL:
                    return null;
                case BoardInformationValueId.EAPI_ID_HWMON_CPU_TEMP:
                    return new SgetDatapointDefinition(new Guid("2C77712B-792E-4525-BF32-43B98ADDB358"), 
                        UicDataType.Double, "HWMON_CPU_TEMP", "CPU Temperature", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_CHIPSET_TEMP:
                    return new SgetDatapointDefinition(new Guid("32B9D71C-FAE4-459A-B55C-F90043B36FD5"), 
                        UicDataType.Double, "HWMON_CHIPSET_TEMP", "Chipset Temperature", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_SYSTEM_TEMP:
                    return new SgetDatapointDefinition(new Guid("2B82E7FC-5CF9-47D7-8628-165E41A4D270"), 
                        UicDataType.Double, "HWMON_SYSTEM_TEMP", "System Temperature", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_VCORE:
                    return new SgetDatapointDefinition(new Guid("EA36D432-D3AA-4FE6-BF1A-5F426391AF29"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_VCORE", "VCore  Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_2V5:
                    return new SgetDatapointDefinition(new Guid("86AF29CE-1B16-4389-BE3E-B16177AEE8E0"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_2V5", "2V5 Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_3V3:
                    return new SgetDatapointDefinition(new Guid("2C4BCA30-D414-44E2-8175-4D8EA4FD9AD9"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_3V3", "3V3  Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_VBAT:
                    return new SgetDatapointDefinition(new Guid("1F2B7CFA-4B49-438A-AE0D-D1FC05619C8A"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_VBAT", "VBat Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_5V:
                    return new SgetDatapointDefinition(new Guid("306D9B8C-47B9-471E-BFCE-46D91379495A"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_5V", "5V Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_5VSB:
                    return new SgetDatapointDefinition(new Guid("9BDDE406-2A79-49FC-A00E-0125855BD7E9"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_5VSB", "5VSB Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_12V:
                    return new SgetDatapointDefinition(new Guid("442911D3-BE9B-4128-80CF-002AEAAD1D61"), 
                        UicDataType.Integer, "HWMON_VOLTAGE_12V", "12V Voltage", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_FAN_CPU:
                    return new SgetDatapointDefinition(new Guid("1DB26A04-AE5A-4802-9755-9E411D4B7081"), 
                        UicDataType.Integer, "HWMON_FAN_CPU", "CPU Fan", string.Empty);
                case BoardInformationValueId.EAPI_ID_HWMON_FAN_SYSTEM:
                    return new SgetDatapointDefinition(new Guid("04AEECAC-C183-4910-9C2F-7C1E9C4643B8"), 
                        UicDataType.Integer, "HWMON_FAN_SYSTEM", "System Fan", string.Empty);
                default:
                    throw new ArgumentOutOfRangeException(nameof(item), item, null);
            }
        }

        private AttribtueDefinition GetAttributeDefinitionOf(BoardInformationValueId item)
        {
            switch (item) {
                case BoardInformationValueId.EAPI_ID_GET_EAPI_SPEC_VERSION:
                    return new SgetAttributDefinition(new Guid("577FEDB1-9D53-421B-9821-104E04D97343"), "EAPI_SPEC_VERSION", UicDataType.Integer, string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_PNPID_VAL:
                    return new SgetAttributDefinition(new Guid("61C8382D-CF27-4F53-B06C-40BC458DBE2A"), "BOARD_PNPID", UicDataType.Integer, string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_PLATFORM_REV_VAL:
                    return new SgetAttributDefinition(new Guid("EE24F7B9-46AA-41E7-902D-A20AB316172D"), "PLATFORM_REV", UicDataType.Integer, string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_DRIVER_VERSION_VAL:
                    return new SgetAttributDefinition(new Guid("D257CEFB-BA12-4637-988F-78BB8FE79BB9"), "DRIVER_VERSION", UicDataType.Integer, string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_LIB_VERSION_VAL:
                    return new SgetAttributDefinition(new Guid("B2BAC5B4-48B7-4F85-BC25-3384B2B9C415"), "LIB_VERSION", UicDataType.Integer, string.Empty);
                case BoardInformationValueId.EAPI_ID_BOARD_BOOT_COUNTER_VAL:
                case BoardInformationValueId.EAPI_ID_BOARD_RUNNING_TIME_METER_VAL:
                case BoardInformationValueId.EAPI_ID_HWMON_CPU_TEMP:
                case BoardInformationValueId.EAPI_ID_HWMON_CHIPSET_TEMP:
                case BoardInformationValueId.EAPI_ID_HWMON_SYSTEM_TEMP:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_VCORE:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_2V5:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_3V3:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_VBAT:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_5V:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_5VSB:
                case BoardInformationValueId.EAPI_ID_HWMON_VOLTAGE_12V:
                case BoardInformationValueId.EAPI_ID_HWMON_FAN_CPU:
                case BoardInformationValueId.EAPI_ID_HWMON_FAN_SYSTEM:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(item), item, null);
            }
        }
        
        private AttribtueDefinition GetAttributeDefinitionOf(BoardInformationStringId item) {
            switch (item) {
                case BoardInformationStringId.EAPI_ID_BOARD_MANUFACTURER_STR:
                    return new SgetAttributDefinition(new Guid("{6beb6a88-8061-4bdf-a900-1efd70a6686d}"), "MANUFACTURER", UicDataType.String, String.Empty);
                case BoardInformationStringId.EAPI_ID_BOARD_NAME_STR:
                    return new SgetAttributDefinition(new Guid("{22e6f4fe-03e7-4c6d-b6f5-5cbf0ab9f9c6}"), "NAME", UicDataType.String, String.Empty);
                case BoardInformationStringId.EAPI_ID_BOARD_REVISION_STR:
                    return new SgetAttributDefinition(new Guid("{c0adb64c-5a8a-4653-b42f-580b19a60d1c}"), "REVISION", UicDataType.String, String.Empty);
                case BoardInformationStringId.EAPI_ID_BOARD_SERIAL_STR:
                    return new SgetAttributDefinition(new Guid("{a65a6538-96d1-4525-b0f2-5059dfa38e0e}"), "SERIAL", UicDataType.String, String.Empty);
                case BoardInformationStringId.EAPI_ID_BOARD_BIOS_REVISION_STR:
                    return new SgetAttributDefinition(new Guid("{ffd2c0a2-c3be-43b6-8fbf-3a3bd962356c}"), "BIOS_REVISION", UicDataType.String, String.Empty);
                case BoardInformationStringId.EAPI_ID_BOARD_HW_REVISION_STR:
                    return new SgetAttributDefinition(new Guid("{edb7802a-5e66-4b24-8e6e-4e2876b772f1}"), "HW_REVISION", UicDataType.String, String.Empty);
                case BoardInformationStringId.EAPI_ID_BOARD_PLATFORM_TYPE_STR:
                    return new SgetAttributDefinition(new Guid("{ec7a1668-353f-471b-a3fe-32ce072701c8}"), "PLATFORM_TYPE", UicDataType.String, String.Empty);
                default:
                    throw new ArgumentOutOfRangeException(nameof(item), item, null);
            }
        }

        
    }
}