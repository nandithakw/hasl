using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Xcendant.HASL.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HemophiliaType
    {
        A = 0, B = 1
    }
}
