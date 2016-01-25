using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Xcendant.HASL.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IdentificationType
    {
        NIC = 0, PASSPORT = 1, OTHER = 9
    }
}
