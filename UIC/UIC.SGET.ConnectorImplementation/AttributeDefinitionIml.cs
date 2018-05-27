using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;

namespace UIC.SGET.ConnectorImplementation
{
    public class AttributeDefinitionImpl : AttributeDefinition
    {

        public AttributeDefinitionImpl(Guid Id, string Label, string Description, UicDataType DataType, string Uri)
        {
            this.Id = Id;
            this.Label = Label;
            this.Description = Description;
            this.DataType = DataType;
            this.Uri = Uri;


        }



        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public UicDataType DataType { get; set; }

        public string Uri { get; set; }
    }
}
