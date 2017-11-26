namespace UIC.EDM.EApi.BoardInformation.EApi.BoardInformation
{
    internal enum BoardInformationValueId : uint {
        EAPI_ID_GET_EAPI_SPEC_VERSION = 0, /* EAPI Specification 
                                                                * Revision I.E. The 
                                                                * EAPI Spec Version 
                                                                * Bits 31-24, Revision 
                                                                * 23-16, 15-0 always 0
                                                                * Used to implement 
                                                                * this interface
                                                                */

        EAPI_ID_BOARD_BOOT_COUNTER_VAL = 1, /* Units = Boots */
        EAPI_ID_BOARD_RUNNING_TIME_METER_VAL = 2, /* Units = Minutes */
        EAPI_ID_BOARD_PNPID_VAL = 3, /* Encoded PNP ID 
                                                                * Format 
                                                                * (Compressed ASCII,
                                                                */
        EAPI_ID_BOARD_PLATFORM_REV_VAL = 4, /* Platform Revision 
                                                                * I.E. The PICMG Spec 
                                                                * Version Bits 31-24,
                                                                * Revision 23-16, 
                                                                * 15-0 always 0
                                                                */

        EAPI_ID_BOARD_DRIVER_VERSION_VAL = 0x10000, /* Vendor Specific 
                                                                  * (Optional, 
                                                                  */
        EAPI_ID_BOARD_LIB_VERSION_VAL = 0x10001, /* Vendor Specific
                                                                  * (Optional, 
                                                                  */

        EAPI_ID_HWMON_CPU_TEMP = 0x20000, /* 0.1 Kelvins */
        EAPI_ID_HWMON_CHIPSET_TEMP = 0x20001, /* 0.1 Kelvins */
        EAPI_ID_HWMON_SYSTEM_TEMP = 0x20002, /* 0.1 Kelvins */

        EAPI_ID_HWMON_VOLTAGE_VCORE = 0x21004, /* millivolts */
        EAPI_ID_HWMON_VOLTAGE_2V5 = 0x21008, /* millivolts */
        EAPI_ID_HWMON_VOLTAGE_3V3 = 0x2100C, /* millivolts */
        EAPI_ID_HWMON_VOLTAGE_VBAT = 0x21010, /* millivolts */
        EAPI_ID_HWMON_VOLTAGE_5V = 0x21014, /* millivolts */
        EAPI_ID_HWMON_VOLTAGE_5VSB = 0x21018, /* millivolts */
        EAPI_ID_HWMON_VOLTAGE_12V = 0x2101C, /* millivolts */

        EAPI_ID_HWMON_FAN_CPU = 0x22000, /* RPM */
        EAPI_ID_HWMON_FAN_SYSTEM = 0x22001, /* RPM */

    }
}