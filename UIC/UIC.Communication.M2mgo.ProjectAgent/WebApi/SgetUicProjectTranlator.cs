using System;
using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Command;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi
{
    internal class SgetUicProjectTranlator
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, AttributeDefinition> _guidUicAttributeMap  = new Dictionary<string, AttributeDefinition>();
        private readonly Dictionary<string, DatapointDefinition> _guidUicDatapointMap  = new Dictionary<string, DatapointDefinition>();
        
        public SgetUicProjectTranlator(EmbeddedDriverModule[] modules, ILogger logger) {
            _logger = logger;
            foreach (var embeddedDriverModule in modules) {
                var edmCapability = embeddedDriverModule.GetCapability();
                 BuildEdmMap(edmCapability);
            }
        }
        public UicProject Translate(SgetProject sgetProject) {
            var attributes = new List<AttributeDefinition>();
            foreach (var property in sgetProject.Properties) {
                AttributeDefinition definition;
                if (_guidUicAttributeMap.TryGetValue(property.Info.Key, out definition)) {
                    attributes.Add(definition);
                }
                else {
                    _logger.Warning("Cannot translate sget project property " + property.AttributeID + " - " + property.Info.Name);
                }
                
            }

            var datapointTasks = new List<ProjectDatapointTask>();
            foreach (var task in sgetProject.DataPointTasks) {
                DatapointDefinition datapointDefinition;
                if (!_guidUicDatapointMap.TryGetValue(task.Info.Key, out datapointDefinition)) {
                    _logger.Warning("Cannot translate sget project property " + task.Info.Name);
                    continue;
                }

                DatapointTaskReportingCondition reportingCondition = GetSgetCloudReportingCondition(task.CloudCondition);
                DatapointTaskMetadata metaData = GetSgetCloudViewMetadata(task.ViewMetaData);
                datapointTasks.Add(new SgetProjectDatapointTask(datapointDefinition, reportingCondition, task.PollIntervall, metaData, task.Description));
            }

            return new SgetUicProject(sgetProject.ProjectKey, sgetProject.Name, sgetProject.Description, sgetProject.Owner, sgetProject.CustomerForeignKey, attributes, datapointTasks);
        }

    

        private SgetDatapointTaskReportingCondition GetSgetCloudReportingCondition(SgetCloudReportingCondition condition) {
            return new SgetDatapointTaskReportingCondition(condition.ReportingThresholdInPercent, condition.MinimalAbsoluteDifference, condition.ReportingThresholdInMilliSecs);
        }

        private DatapointTaskMetadata GetSgetCloudViewMetadata(SgetCloudViewMetadata data) {
            return new SgetDatapointTaskMetadata(data.Max ?? 0, data.Min ?? 100, data.WarningThreshold ?? 80, data.ErrorThreshold ?? 90, data.IsInverseThresholdEvaluation, data.Tags.FirstOrDefault());
        }

        public SgetEmbeddedDriverModuleAppliance Translate(EdmCapability edmCapability) {
            var cloudMapperIdentifier = new CloudMapperIdentifier(new Guid("9D368ED2-075F-4348-831E-D2CEA97E881A"), "SgetUicProjectTranlator");
            var embeddedModuleIdentifier = new EmbeddedModuleIdentifier(edmCapability.Identifier.Id, edmCapability.Identifier.Uri);
            var interfaceIdentifier = new EmbeddedHwInterfaceIdentifier(embeddedModuleIdentifier.Id, embeddedModuleIdentifier.Name);
            
            var attributes = Translate(edmCapability.AttributeDefinitions, interfaceIdentifier);
            var dataPoints = Translate(edmCapability.DatapointDefinitions, interfaceIdentifier);
            var commands = Translate(edmCapability.CommandDefinitions, interfaceIdentifier, dataPoints);
            var embeddedModuleHwInterfaceCapability = new EmbeddedModuleHwInterfaceCapability(interfaceIdentifier, attributes, dataPoints, commands);
            var interfaces = new List<EmbeddedModuleHwInterfaceCapability> {
                embeddedModuleHwInterfaceCapability
            };
            
            var embeddedDriverModulCapabilitiy = new EmbeddedDriverModulCapabilitiy(embeddedModuleIdentifier, interfaces);
            return new SgetEmbeddedDriverModuleAppliance(cloudMapperIdentifier, new []{embeddedDriverModulCapabilitiy});
        }

        private List<SGetCloudMapperCommandDefinition> Translate(CommandDefinition[] edmCapabilityDatapointDefinitions, EmbeddedHwInterfaceIdentifier interfaceIdentifier, List<SGetCloudMapperDataPointDefinition> sensors) {
            var result = new List<SGetCloudMapperCommandDefinition>();
            foreach (var item in edmCapabilityDatapointDefinitions)
            {
                SGetCloudMapperDataPointDefinition sensor = null;
                if (item.RelatedDatapoint != null) {
                    sensor = sensors.SingleOrDefault(s => s.Key == item.RelatedDatapoint.Uri);
                }
                if(sensor != null)
                {
                    result.Add(new SGetCloudMapperCommandDefinition(item.Uri, item.Command, interfaceIdentifier, item.Tags, sensor));
                }
            }
            return result;
            //return edmCapabilityDatapointDefinitions.Select(a => {
            //    SGetCloudMapperDataPointDefinition sensor = sensors.SingleOrDefault(s => s.Key == a.RelatedDatapoint.Uri);
            //    return new SGetCloudMapperCommandDefinition(a.Uri, a.Command, interfaceIdentifier, a.Tags, sensor);
            //}).ToList();
        }

        private List<SGetCloudMapperDataPointDefinition> Translate(DatapointDefinition[] edmCapabilityDatapointDefinitions, EmbeddedHwInterfaceIdentifier interfaceIdentifier) {
            return edmCapabilityDatapointDefinitions.Select(a => new SGetCloudMapperDataPointDefinition(a.Uri, a.Label, a.Description, Translate(a.DataType), interfaceIdentifier)).ToList();
        }

        private SGetCloudMapperDataType Translate(UicDataType dataType) {
            switch (dataType) {
                case UicDataType.String:
                    return SGetCloudMapperDataType.String;
                case UicDataType.Integer:
                    return SGetCloudMapperDataType.Int;
                case UicDataType.Double:
                    return SGetCloudMapperDataType.Double;
                case UicDataType.Byte:
                    return SGetCloudMapperDataType.Unknown;
                case UicDataType.Gps:
                    return SGetCloudMapperDataType.Gps;
                case UicDataType.Picture:
                    return SGetCloudMapperDataType.Unknown;
                case UicDataType.Bool:
                    return SGetCloudMapperDataType.Boolean;
                case UicDataType.Unknown:
                    return SGetCloudMapperDataType.Unknown;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            }
        }

        private List<SGetCloudMapperAttributeDefinition> Translate(AttributeDefinition[] edmCapabilityAttribtueDefinitions, EmbeddedHwInterfaceIdentifier interfaceidentifier) {
            return edmCapabilityAttribtueDefinitions.Select(a => new SGetCloudMapperAttributeDefinition(a.Uri, a.Label, a.Description, interfaceidentifier)).ToList();
        }

        private void BuildEdmMap(EdmCapability edmCapability) {
            foreach (var definition in edmCapability.AttributeDefinitions) {
                _guidUicAttributeMap.Add(definition.Uri, definition);
                if (definition.Id == new Guid("(b68df3f9-4748-4c9d-9bda-567c87fab855)")) {
                    _guidUicAttributeMap.Add("Id", definition);
                }
            }

            foreach (var definition in edmCapability.DatapointDefinitions) {
                _guidUicDatapointMap.Add(definition.Uri, definition);
                if (definition.Id == new Guid("{83f02bea-c22b-46aa-b1c2-4ab8102d8a80}")) {
                    _guidUicDatapointMap.Add("bool_mock", definition);
                } else if (definition.Id == new Guid("{4087d40d-d4e2-42b1-89a4-9b9d18499a04}")) {
                    _guidUicDatapointMap.Add("int_mock", definition);
                } else if (definition.Id == new Guid("{a41fc3af-4f73-42bf-8290-43ed883edd8f}")) {
                    _guidUicDatapointMap.Add("double_mock", definition);
                } else if (definition.Id == new Guid("{fbd3e390-ffb7-455b-b0dc-695b13329eb6}")) {
                    _guidUicDatapointMap.Add("string_mock", definition);
                }
            }
        }
    }
}