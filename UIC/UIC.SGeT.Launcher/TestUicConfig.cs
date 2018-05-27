using System;
using System.Collections.Generic;
using UIC.Communication.M2mgo.CommunicationAgent;
using UIC.Communication.M2mgo.ProjectAgent;
using UIC.EDM.EApi.BoardInformation;
using UIC.EDM.EApi.Gpio;
using UIC.EDM.EApi.I2c.Adafruit.VCNL4010;
using UIC.EDM.System.Reboot;
using UIC.EDM.Test.Mockup;
using UIC.Framework.Interfaces;
using UIC.Framework.Interfaces.Communication.Application;
using UIC.Framework.Interfaces.Communication.Projects;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Edm;
using UIC.SGET.ConnectorImplementation;
using UIC.Util;
using UIC.Util.Logging;
using UIC.Util.Serialization;
using HAW.AWS.CommunicationAgent;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.SGeT.Launcher
{
    public class TestUicConfig : UicProject
    {
        public string ProjectKey { get;  set;  }

        public string Name { get; set;   }

        public string Description { get;  set;  }

        public string Owner { get;  set;  }

        public Guid CustomerForeignKey { get; set; }

        public List<AttributeDefinition> Attributes { get; set;  }

        public List<ProjectDatapointTask> DatapointTasks { get; set;  }
    }
}
