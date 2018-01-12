using System;
using System.Collections.Generic;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto {
    public class BlueprintDto {
        public Identifier Identifier { get; set; }
        public Identifier DomainIdentifier { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public bool IsSelfRegisteringAllowed { get; set; }
        public int MaxDevices { get; set; }
        public List<Sensor> Sensors { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<CommandDto> Commands { get; set; }

        public void PrepareMasterForCreation()
        {
            CreationDate = DateTime.UtcNow.ToString("O");
        }
    }
}
