using System;

namespace UIC.EDM.EApi.Gpio.Eapi
{
    public enum EapiGpioId : uint
    {
        EAPI_ID_GPIO_GPIO00 = 0,
        EAPI_ID_GPIO_GPIO01 = 1,
        EAPI_ID_GPIO_GPIO02 = 2,
        EAPI_ID_GPIO_GPIO03 = 3,
        EAPI_ID_GPIO_GPIO04 = 4,
        EAPI_ID_GPIO_GPIO05 = 5,
        EAPI_ID_GPIO_GPIO06 = 6,
        EAPI_ID_GPIO_GPIO07 = 7,
        EAPI_ID_GPIO_GPIO08 = 8,
        EAPI_ID_GPIO_GPIO09 = 9,
        EAPI_ID_GPIO_GPIO10 = 10,
        EAPI_ID_GPIO_GPIO11 = 11,
        EAPI_ID_GPIO_GPIO12 = 12,
        EAPI_ID_GPIO_GPIO13 = 13,
        EAPI_ID_GPIO_GPIO14 = 14,
        EAPI_ID_GPIO_GPIO15 = 15,
    }

    public static class EapiGpioIdEx
    {
        public static Guid ToGuid(this EapiGpioId gpio) {
            switch (gpio) {
                case EapiGpioId.EAPI_ID_GPIO_GPIO00:
                    return new Guid("{9acd4b65-765c-4d1f-ae85-a81a0d2cafb8}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO01:
                    return new Guid("{8de0628c-da36-4e51-940d-13dc0419f356}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO02:
                    return new Guid("{31fcc6fc-efad-4971-978e-d045e5d2158f}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO03:
                    return new Guid("{da03dad5-2001-4f36-96ac-331b2c1d9a2f}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO04:
                    return new Guid("{f60ba4ad-6a75-448d-9ea8-868d6517f89c}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO05:
                    return new Guid("{3fc88d2c-5c4f-4ce0-8655-30c2e0ea3572}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO06:
                    return new Guid("{3ca34fae-8680-4114-bbff-fbaf83a7e8c9}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO07:
                    return new Guid("{81264ee6-5a13-4605-a3df-31367ca7905e}");
                case EapiGpioId.EAPI_ID_GPIO_GPIO08:
                    return new Guid("CE01AE44-4D49-4F47-A9B1-37460BECEF6C");
                case EapiGpioId.EAPI_ID_GPIO_GPIO09:
                    return new Guid("DF00F017-5C8B-48C4-8658-022565D32F2F");
                case EapiGpioId.EAPI_ID_GPIO_GPIO10:
                    return new Guid("25660E03-4944-4238-9FAD-EA604E6BF7A4");
                case EapiGpioId.EAPI_ID_GPIO_GPIO11:
                    return new Guid("F4624D52-D4B7-4997-A09E-9E9F848EB77A");
                case EapiGpioId.EAPI_ID_GPIO_GPIO12:
                    return new Guid("11FD5AD2-4C74-46D4-AED1-27BC442B846B");
                case EapiGpioId.EAPI_ID_GPIO_GPIO13:
                    return new Guid("290226FA-271F-48ED-8D38-891612C72BBF");
                case EapiGpioId.EAPI_ID_GPIO_GPIO14:
                    return new Guid("5A80B7D4-98B5-4CB8-A108-7709BBE080EB");
                case EapiGpioId.EAPI_ID_GPIO_GPIO15:
                    return new Guid("2FAA7922-09F4-40F7-BAA8-345EE5B727DA");
                default:
                    throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null);
            }
        }
        public static Guid ToOnCommandGuid(this EapiGpioId gpio) {
            switch (gpio) {
                case EapiGpioId.EAPI_ID_GPIO_GPIO00:
                    return new Guid("FBAE9476-2BA0-41A3-BE8D-41A34F7CA6A4");
                case EapiGpioId.EAPI_ID_GPIO_GPIO01:
                    return new Guid("6E52E734-9FBA-42D1-A8B6-4ACB213B71F5");
                case EapiGpioId.EAPI_ID_GPIO_GPIO02:
                    return new Guid("25380113-F0DA-4ED8-B88E-5D15429C9A3F");
                case EapiGpioId.EAPI_ID_GPIO_GPIO03:
                    return new Guid("031B98B9-7239-45CD-9628-5D429F6D3CBD");
                case EapiGpioId.EAPI_ID_GPIO_GPIO04:
                    return new Guid("2BCA4602-AA2D-4FC4-A364-613125706121");
                case EapiGpioId.EAPI_ID_GPIO_GPIO05:
                    return new Guid("90D4C5B9-D6CA-458F-A0E4-F7F3C51FBE67");
                case EapiGpioId.EAPI_ID_GPIO_GPIO06:
                    return new Guid("6E14D1A4-A964-44EF-921B-3016ECAA622F");
                case EapiGpioId.EAPI_ID_GPIO_GPIO07:
                    return new Guid("E77C0D91-8091-45A8-B516-301914C3F2CD");
                case EapiGpioId.EAPI_ID_GPIO_GPIO08:
                    return new Guid("BC4F788B-6337-4BBC-AFA1-21AEC3D72FEA");
                case EapiGpioId.EAPI_ID_GPIO_GPIO09:
                    return new Guid("364E5127-DC15-4510-A3F1-510E18D7FCEA");
                case EapiGpioId.EAPI_ID_GPIO_GPIO10:
                    return new Guid("B072D87B-0A06-481C-987E-99C6EE94134B");
                case EapiGpioId.EAPI_ID_GPIO_GPIO11:
                    return new Guid("DC529196-9DE7-4E8C-BB5B-E414DC228CED");
                case EapiGpioId.EAPI_ID_GPIO_GPIO12:
                    return new Guid("8E70D34A-E1A8-46C7-AE05-9D360293BE5B");
                case EapiGpioId.EAPI_ID_GPIO_GPIO13:
                    return new Guid("F77636C8-C7E9-464F-95C1-9A95A445142C");
                case EapiGpioId.EAPI_ID_GPIO_GPIO14:
                    return new Guid("0FB944FD-B00B-4E70-BA49-454037A88E40");
                case EapiGpioId.EAPI_ID_GPIO_GPIO15:
                    return new Guid("20B972B7-B8F5-42FF-9DFF-2E4917799FD8");
                default:
                    throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null);
            }
        }

        public static Guid ToOffCommandGuid(this EapiGpioId gpio) {
            switch (gpio) {
                case EapiGpioId.EAPI_ID_GPIO_GPIO00:
                    return new Guid("AE951D99-6AD0-4566-85A7-25FF50158725");
                case EapiGpioId.EAPI_ID_GPIO_GPIO01:
                    return new Guid("873C4B9A-265B-4ACA-945E-CE3B38309B72");
                case EapiGpioId.EAPI_ID_GPIO_GPIO02:
                    return new Guid("EC8D783C-0A89-41EF-8631-99A10FE06BE4");
                case EapiGpioId.EAPI_ID_GPIO_GPIO03:
                    return new Guid("306C8D86-7DFD-4BC2-97AA-D7B34F07B15A");
                case EapiGpioId.EAPI_ID_GPIO_GPIO04:
                    return new Guid("98431863-DFA0-4D29-BFFD-5C6CE70B2ED6");
                case EapiGpioId.EAPI_ID_GPIO_GPIO05:
                    return new Guid("9AC2E14B-CD5C-4BD3-8B96-B19DA8701F70");
                case EapiGpioId.EAPI_ID_GPIO_GPIO06:
                    return new Guid("115439F5-85FB-4AE4-BF99-345BB4E8ECEC");
                case EapiGpioId.EAPI_ID_GPIO_GPIO07:
                    return new Guid("B1B71E71-655A-43DB-AA7A-5C8185E528E1");
                case EapiGpioId.EAPI_ID_GPIO_GPIO08:
                    return new Guid("BEB8C84C-15DA-427D-8C1B-8DB6FEB5CDC3");
                case EapiGpioId.EAPI_ID_GPIO_GPIO09:
                    return new Guid("6BAB514F-46FB-4C03-885F-D601BD3966C9");
                case EapiGpioId.EAPI_ID_GPIO_GPIO10:
                    return new Guid("6C18EF9B-D8CB-4994-B055-9EDB00B7A8DE");
                case EapiGpioId.EAPI_ID_GPIO_GPIO11:
                    return new Guid("04B398C2-B10D-472E-B075-295F82F06222");
                case EapiGpioId.EAPI_ID_GPIO_GPIO12:
                    return new Guid("ADCE3E28-2325-4A98-8078-E93699CF694E");
                case EapiGpioId.EAPI_ID_GPIO_GPIO13:
                    return new Guid("97213627-4291-4582-8BB0-B058ABD21BCC");
                case EapiGpioId.EAPI_ID_GPIO_GPIO14:
                    return new Guid("F56498F3-FAF9-4622-B7BE-035C624DC025");
                case EapiGpioId.EAPI_ID_GPIO_GPIO15:
                    return new Guid("B3E28AB3-0CE5-4DC9-A885-67F07121110D");
                default:
                    throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null);
            }
        }
    }
}
