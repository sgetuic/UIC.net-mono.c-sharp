
using System;
using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using Attribute = UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto.Attribute;


namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.DeviceManagement
{
    internal class M2MgoProjectBlueprintTranslator
    {
        internal BlueprintDto UpdateProjectDomain(BlueprintDto exisingBlueprint, UicProject project)
        {
            ProjectDatapointTask[] allDataPointTasks = project.DatapointTasks.ToArray();
            var newSensorList = new List<Sensor>();
            foreach (Sensor item in exisingBlueprint.Sensors)
            {
                if (allDataPointTasks.Any(d => GetKeyFrom(d.Definition) == item.SensorKey))
                {
                    newSensorList.Add(item);
                }
            }
            exisingBlueprint.Sensors = newSensorList;
            foreach (var datapoint in allDataPointTasks)
            {
                UdpateSensorAndCorrespondingCommands(exisingBlueprint, datapoint);
            }


            foreach (AttributeDefinition property in project.Attributes)
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

        private IEnumerable<CommandDto> GetCommandsOf(ProjectDatapointTask dataPoint)
        {
            throw new NotImplementedException();
            //IEnumerable<CommandDto> commandDtos = project.GetCommandsFor(datapoint).Select(c => new CommandDto
            //{
            //    Command = c.InterfaceIdentifier.Name + "." + c.Command,
            //    Name = c.Name,
            //    Metadata = new CommandMetadataViewModel
            //    {
            //        RelatedToSensorKey = c.RelatedSensorKey.Return(GetKeyFrom(c), ""),
            //        Tags = c.Tags
            //    }
            //}).ToArray();
            //return commandDtos;
        }

        private Attribute GetAttributesOf(AttributeDefinition attribute)
        {
            return new Attribute
            {
                Description = attribute.Description,
                Name = GetKeyFrom(attribute)
            };
        }

        private Sensor GetSensorsOf(ProjectDatapointTask dataPoint)
        {
            Sensor sensor = new Sensor
            {
                DataType = GetM2mgoDataTypeOf(dataPoint.Definition.DataType),
                Description = dataPoint.Description,
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

        public string GetKeyFrom(DatapointDefinition definition)
        {

            return definition.Uri;
        }

        public string GetKeyFrom(AttributeDefinition definition)
        {
            return definition.Uri;
        }


        
    }   
}
