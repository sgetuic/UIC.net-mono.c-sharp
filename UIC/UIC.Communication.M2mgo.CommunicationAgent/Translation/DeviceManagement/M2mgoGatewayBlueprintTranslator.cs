using System;
using System.Collections.Generic;
using UIC.Communication.M2mgo.CommunicationAgent.Translation.Project;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto;
using UIC.Framework.Interfaces.Project;
using Attribute = UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto.Attribute;

namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.DeviceManagement
{
    internal class M2mgoGatewayBlueprintTranslator
    {
        internal const string CloudMapperGatewayAnchorBlueprintCode = "CloudMapper Gateway";
        private readonly M2MgoProjectTranslator _projectTranslator;

        public M2mgoGatewayBlueprintTranslator(M2MgoProjectTranslator projectTranslator)
        {
            _projectTranslator = projectTranslator;
        }

        internal const string SensorKeyDebug = "debug";
        internal const string SensorKeyDebugEnabled = "debug enabled";
        internal const string AttributeKeySerialId = "Serial";
        internal const string AttributeKeyProjectKey = "ProjectKey";
        internal const string AttributeKeyProjectName = "ProjectName";
        internal const string AttributeKeyCustomer = "Customer";
        internal const string CommandKeyDebugOn = "Debug ON";
        internal const string CommandKeyDebugOff = "Debug OFF";

        

        internal BlueprintDto GetGatewayBlueprintDefiniton(UicProject project)
        {
            const string name = "CloudMapper Gateway";
            const string code = CloudMapperGatewayAnchorBlueprintCode;
            const string label = name;
            
            var sensors = new List<Sensor>
            {
                new Sensor
                {
                    DataType = (int) SensorDataType.String,
                    Description = String.Empty,
                    Icon = String.Empty,
                    Name = "Debug Channel",
                    SensorKey = SensorKeyDebug,
                    Units = String.Empty,
                },
                new Sensor
                {
                    DataType = (int) SensorDataType.Boolean,
                    Description = String.Empty,
                    Icon = String.Empty,
                    Name = "Debug Enabled",
                    SensorKey = SensorKeyDebugEnabled,
                    Units = String.Empty,
                },
            };
            var commands = new List<CommandDto>
            {
                new CommandDto
                {
                    Command = "board.debug@on",
                    Name = CommandKeyDebugOn,
                },
                new CommandDto
                {
                    Command = "board.debug@off",
                    Name = CommandKeyDebugOff,
                },
            };
            var attributes = new List<Attribute> {
                new Attribute
                {
                    Name = AttributeKeySerialId,
                },
                new Attribute
                {
                    Name = AttributeKeyProjectKey,
                },
                new Attribute
                {
                    Name = AttributeKeyProjectName,
                },
                new Attribute
                {
                    Name = AttributeKeyCustomer,
                },
            };
            return new BlueprintDto
            {
                Attributes = attributes,
                Commands = commands,
                CreationDate = null,
                Description = null,
                DomainIdentifier = new Identifier
                {
                    ID = _projectTranslator.GetProjectDomain(project)
                },
                IsSelfRegisteringAllowed = true,
                Label = label,
                MaxDevices = 50,
                Code = code,
                Name = name,
                Sensors = sensors,
            };
        }
    }
}