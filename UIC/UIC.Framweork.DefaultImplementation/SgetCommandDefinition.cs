using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetCommandDefinition: CommandDefinition
    {
        public SgetCommandDefinition(Guid id, string uri, string label, string command, UicDataType dataType, string description, DatapointDefinition relatedDatapoint, string[] tags) {
            Label = label;
            Command = command;
            DataType = dataType;
            Description = description;
            RelatedDatapoint = relatedDatapoint;
            Tags = tags;
            Uri = uri;
            Id = id;
        }

        public Guid Id { get; }
        public string Uri { get; }
        public string Label { get; }
        public string Description { get; }
        public string Command { get; }
        public UicDataType DataType { get; }
        public string[] Tags { get; }
        public DatapointDefinition RelatedDatapoint { get; }
    }
}