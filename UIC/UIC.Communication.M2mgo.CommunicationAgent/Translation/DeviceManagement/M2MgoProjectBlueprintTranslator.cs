
using System;
using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Extensions;
using Attribute = UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto.Attribute;


namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.DeviceManagement
{
    internal class M2MgoProjectBlueprintTranslator
    {
        private readonly UicProject _project;
        private readonly Dictionary<Guid, CommandDefinition> _guidUicCommandMap  = new Dictionary<Guid, CommandDefinition>();
        private readonly Dictionary<Guid, List<CommandDefinition>> _guidUicSensorCommandMap  = new Dictionary<Guid, List<CommandDefinition>>();
      

        public M2MgoProjectBlueprintTranslator(UicProject project, List<EmbeddedDriverModule> edms) {
            _project = project;
      
            edms.ForEach(edm => BuildEdmMap(edm.GetCapability()));
        }
        internal BlueprintDto UpdateProjectDomain(BlueprintDto exisingBlueprint)
        {
            ProjectDatapointTask[] allDataPointTasks = _project.DatapointTasks.ToArray();
            foreach (ProjectDatapointTask datapoint in _project.DatapointTasks)
            {
                if (exisingBlueprint.Sensors.All(d => GetKeyFrom(datapoint.Definition) != d.SensorKey))
                {
                    exisingBlueprint.Sensors.Add(GetSensorsOf(datapoint));
                }
            }
            foreach (var datapoint in allDataPointTasks)
            {
                UdpateSensorAndCorrespondingCommands(exisingBlueprint, datapoint);
            }


            foreach (AttributeDefinition property in _project.Attributes)
            {
                Attribute attribute = GetAttributesOf(property);
                if (exisingBlueprint.Attributes.All(s => s.Name != attribute.Name))
                {
                    exisingBlueprint.Attributes.Add(attribute);
                }
            }
            if (exisingBlueprint.Attributes.All(s => s.Name != M2mgoGatewayBlueprintTranslator.AttributeKeySerialId))
            {
                exisingBlueprint.Attributes.Add(new Attribute
                {
                    Description = "Matching Anchor for autoomatic dashboard over gatway datasource",
                    Name = M2mgoGatewayBlueprintTranslator.AttributeKeySerialId
                });
            }
            return exisingBlueprint;
        }

        private void UdpateSensorAndCorrespondingCommands(BlueprintDto exisingBlueprint, ProjectDatapointTask datapoint)
        {
            Sensor sensor = GetSensorsOf(datapoint);
            Sensor existingSensor = exisingBlueprint.Sensors.SingleOrDefault(s => s.SensorKey == sensor.SensorKey);
            if (existingSensor == null)
            {
                exisingBlueprint.Sensors.Add(sensor);
            }
            else
            {
                existingSensor.Metadata = sensor.Metadata;
            }
            AddDistinctCommand(exisingBlueprint.Commands, datapoint);
        }

        internal BlueprintDto GetProjectBlueprintDto(UicProject project, Guid domainId)
        {
            var commands = new List<CommandDto>();
            var attributes = new List<Attribute>();
            var sensors = new List<Sensor>();

            if (project.Attributes != null) {
                foreach (AttributeDefinition attribute in project.Attributes) {
                    attributes.Add(GetAttributesOf(attribute));
                }
                attributes.Add(new Attribute
                {
                    Description = "Matching Anchor for autoomatic dashboard over gatway datasource",
                    Name = M2mgoGatewayBlueprintTranslator.AttributeKeySerialId
                });
            }

            if (project.DatapointTasks != null) {
                foreach (var datapointTask in project.DatapointTasks)
                {
                    Sensor sensor = GetSensorsOf(datapointTask);
                    sensors.Add(sensor);
                    AddDistinctCommand(commands, datapointTask);
                }
            }

            var blueprint = new BlueprintDto
            {
                Commands = commands,
                Attributes = attributes,
                Sensors = sensors,
                DomainIdentifier = new Identifier { ID = domainId},
                Name = GetBlueprintNameFrom(project),
                Label = GetBlueprintNameFrom(project),
                Code = GetBlueprintCodeFrom(project),
                MaxDevices = 50,
            };
            return blueprint;
        }

        private void AddDistinctCommand(List<CommandDto> commands, ProjectDatapointTask dataPoint)
        {
            IEnumerable<CommandDto> cmds = GetCommandsOf(dataPoint);
            foreach (var cmd in cmds)
            {
                if(commands.All(c => c.Command != cmd.Command))
                    commands.Add(cmd);
            }
        }

        public Command GetCommandFromPayload(string mqttpayload) {
            if (mqttpayload.IsNullOrEmpty()) return null;

            var indexOf = mqttpayload.IndexOf('.');
            var id = new Guid(mqttpayload.Substring(0, indexOf));
            var command = mqttpayload.Substring(indexOf+1);

            var commandDefinition = _guidUicCommandMap[id];

            return new SgetCommand(commandDefinition, command);
        }

        private IEnumerable<CommandDto> GetCommandsOf(ProjectDatapointTask dataPoint)
        {
            List<CommandDefinition> commandDefinitions;
            if (_guidUicSensorCommandMap.TryGetValue(dataPoint.Definition.Id, out commandDefinitions)) {
                IEnumerable<CommandDto> commandDtos = commandDefinitions.Select(c => new CommandDto
                {
                    Command = c.Id + "." + c.Command,
                    Name = c.Label,
                    Metadata = new CommandMetadataViewModel
                    {
                        RelatedToSensorKey = GetKeyFrom(c.RelatedDatapoint),
                        Tags = c.Tags
                    }
                }).ToArray();
                return commandDtos;
            }

            return new CommandDto[0];
        }

        private Attribute GetAttributesOf(AttributeDefinition attribute)
        {
            return new Attribute
            {
                Description = attribute.Uri,
                Name = GetKeyFrom(attribute)
            };
        }

        private Sensor GetSensorsOf(ProjectDatapointTask dataPoint)
        {
            Sensor sensor = new Sensor
            {
                DataType = GetM2mgoDataTypeOf(dataPoint.Definition.DataType),
                Description = dataPoint.Definition.Uri,
                Name = dataPoint.Definition.Label,
                SensorKey = GetKeyFrom(dataPoint.Definition),
                Metadata = new SensorMetadataViewModel
                {
                    ErrorThreshold = dataPoint.MetaData.ErrorThreshold,
                    Tags = new []{dataPoint.MetaData.Tags},
                    IsInverseThresholdEvaluation = dataPoint.MetaData.IslnverseThresholdEvaluation,
                    Max = dataPoint.MetaData.ExpectedMaximum,
                    Min = dataPoint.MetaData.ExpectedMinimum,
                    WarningThreshold = dataPoint.MetaData.WarningThreshold,
                }
            };
            return sensor;
        }

        private int GetM2mgoDataTypeOf(UicDataType type)
        {
            switch (type)
            {
                case UicDataType.Unknown:
                    return (int)SensorDataType.Unknown;
                case UicDataType.Integer:
                    return (int)SensorDataType.Integer;
                case UicDataType.Double:
                    return (int)SensorDataType.Double;
                case UicDataType.Bool:
                    return (int)SensorDataType.Boolean;
                case UicDataType.Gps:
                    return (int)SensorDataType.Gps;
                case UicDataType.String:
                    return (int)SensorDataType.String;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        internal string GetBlueprintCodeFrom(UicProject project) {
            return String.Format("{0}@{1}@project", project.ProjectKey, project.CustomerForeignKey);
        }

        private string GetBlueprintNameFrom(UicProject project) {
            return String.Format("{0}: {1} Project", project.Owner, project.Name);
        }

        public string GetKeyFrom(DatapointDefinition definition) {
            if (definition == null) return string.Empty;
            return definition.Label.Replace(" ", "") + "-" + definition.Id.ToString("N");
        }

        public string GetKeyFrom(AttributeDefinition definition)
        {
            if (definition == null) return string.Empty;
            return definition.Label.Replace(" ", "") + "-" + definition.Id.ToString("N");
        }

        private void BuildEdmMap(EdmCapability edmCapability) {
            foreach (var command in edmCapability.CommandDefinitions) {
                _guidUicCommandMap.Add(command.Id, command);
                if (command.RelatedDatapoint != null) {
                    List<CommandDefinition> commandList;
                    if (_guidUicSensorCommandMap.TryGetValue(command.RelatedDatapoint.Id, out commandList)) {
                        commandList.Add(command);
                    }
                    else {
                        _guidUicSensorCommandMap.Add(command.RelatedDatapoint.Id, new List<CommandDefinition>{command});
                    }
                }
            }
        }
    }   
}
