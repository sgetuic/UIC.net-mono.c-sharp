using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.Framweork.DefaultImplementation
{
    public static class UicUriBuilder
    {
        public static string DatapointFrom(EmbeddedDriverModule edm, string key) {
            return edm.Identifier.Uri + ".datapoint." + key;
        }
        public static string CommandFrom(EmbeddedDriverModule edm, string key) {
            return edm.Identifier.Uri + ".command." + key;
        }
        public static string AttributeFrom(EmbeddedDriverModule edm, string key) {
            return edm.Identifier.Uri + ".attribute." + key;
        }
    }
}
