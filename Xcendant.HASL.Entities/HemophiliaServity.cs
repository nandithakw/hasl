using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Xcendant.HASL.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HemophiliaServity
    {
        MILD, MODERATE, SEVERE
    }
}
