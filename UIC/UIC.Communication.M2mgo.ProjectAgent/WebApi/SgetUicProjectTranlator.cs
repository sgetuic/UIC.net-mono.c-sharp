using System;
using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi
{
    internal class SgetUicProjectTranlator
    {
        private readonly ILogger _logger;
        private readonly Dictionary<Guid, AttributeDefinition> _guidUicAttributeMap  = new Dictionary<Guid, AttributeDefinition>();
        private readonly Dictionary<Guid, DatapointDefinition> _guidUicDatapointMap  = new Dictionary<Guid, DatapointDefinition>();
        
        public SgetUicProjectTranlator(List<EmbeddedDriverModule> modules, ILogger logger) {
            _logger = logger;
            foreach (var embeddedDriverModule in modules) {
                var edmCapability = embeddedDriverModule.GetCapability();
                BuildEdmMap(edmCapability);
            }
        }
        public UicProject Translate(SgetProject sgetProject) {
            var attributes = new List<AttributeDefinition>();
            foreach (var property in sgetProject.Properties) {
                if (_guidUicAttributeMap.TryGetValue(property.AttributeID, out AttributeDefinition definition)) {
                    attributes.Add(definition);
                }
                else {
                    _logger.Warning("Cannot translate sget project property " + property.AttributeID + " - " + property.Info.Name);
                }
                
            }

            var datapointTasks = new List<ProjectDatapointTask>();
            foreach (var task in sgetProject.DataPointTasks) {
                if (!_guidUicDatapointMap.TryGetValue(task.Info.HwInterfaceSchemeID, out DatapointDefinition datapointDefinition)) {
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
            return new SgetDatapointTaskReportingCondition(condition.ReportingThresholdInMilliSecs, condition.ReportingThresholdInPercent, condition.ReportingThresholdInMilliSecs);
        }

        private DatapointTaskMetadata GetSgetCloudViewMetadata(SgetCloudViewMetadata data) {
            return new SgetDatapointTaskMetadata(data.Min ?? 0, data.Max ?? 100, data.WarningThreshold ?? 80, data.ErrorThreshold ?? 90, data.IsInverseThresholdEvaluation, data.Tags.FirstOrDefault());
        }

        public SgetEmbeddedDriverModuleAppliance Translate(EdmCapability edmCapability) {
            throw new NotImplementedException();
        }

        private void BuildEdmMap(EdmCapability edmCapability) {
            foreach (var definition in edmCapability.AttribtueDefinitions) {
                _guidUicAttributeMap.Add(definition.Id, definition);
            }

            foreach (var definition in edmCapability.DatapointDefinitions) {
                _guidUicDatapointMap.Add(definition.Id, definition);
            }
            
        }
    }
}