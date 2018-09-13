# Azure Communication Agent for UIC

`UIC.Communication.Azure.CommunicationAgent` adds connectivity to 
[Microsoft Azure IoT HUB](https://azure.microsoft.com/en-us/services/iot-hub/).

It turns datapoint operations to IoTHub [device-to-cloud messages](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-messages-d2c).

Attribute push operation is converted to reported propery of the specitific [device twin](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-device-twins).

## Configuration

Each UIC EDM must be associated with IoT device registered in the IoT Hub.
`AzureProvisioningAgent.cs` module is responsible for this association.

The implementation supports many-to-one mapping EDMs to a IoTHub device. If multiple EDMsare mapped to a single device all attirbutes from EDMs 
will be merged to the device twin.

### Configuration sample:

```
{
  ""IotHubUri"": ""SGET-UIC-IotHub.azure-devices.net"",
  ""DeviceKeys"": {
    ""uic_device_mockup"": ""u2oby8L4cEG9ivBZzAN1L1OYZs1uGv6SknbGGfFsHp8="",
    ""uic_device_eapi"": ""NPzFCxMyQPPehQVN8LoumZ5qxIKnkwr2+jcspX6yGcI=""
  },
  ""EdmMappings"": {
    ""1e7b861c-d6a0-4993-812e-0ffd42a4c77d"": ""uic_device_mockup"",
    ""b3bb9ffa-43ba-49f9-bbb2-40d1de291215"": ""uic_device_eapi"",
    ""d958bb24-7a1a-4cb6-bb7f-3a5868d3170d"": ""uic_device_eapi""
  }
}
```

IotHubUri must reflect customer's IoT Hub identifucator.

The DeviceKeys contains registered (or in future provisioned) IotHub device ids and their connection strings.

EdmMappings maps EDM by GUID to the IoTHub device.

## Datapoint push implementation

Each datapoint push is converted to device-to-cloud message and contains:

* datapoint GUID
* datapoint label
* string implementation of the datapoint value

Example:

```
'{"Id":"4087d40d-d4e2-42b1-89a4-9b9d18499a04","Label":"Random Integer","Value":"60"}'
```

## Attribute push

Attributes configured in the project are mapped to IotHub devices according to 
the configuration and converted to reported properties of the secific device twin.

Example:

```
 "reported": {
      "MANUFACTURER": "Kontron AG",
      "NAME": "KEAPI3 Simulator (MOCK)",
      "SERIAL": "SN123456789",
      "BIOS_REVISION": "build 001",
      "EAPI_SPEC_VERSION": "16777216",
      "LIB_VERSION": "16777216",
```

## Limitations

Azure IoT Hub Device Provisioning Service (https://docs.microsoft.com/en-us/azure/iot-dps/) is not implemented, TBD.

Only symmetric keys are supported for the device connection string.

EDM Command not mapped. Futire implementation must convert
 [Azure IotHub device direct method](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-direct-methods) to EDM command

