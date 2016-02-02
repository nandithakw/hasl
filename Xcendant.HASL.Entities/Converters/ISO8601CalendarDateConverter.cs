using Newtonsoft.Json.Converters;

namespace Xcendant.HASL.Entities.Converters
{
    public class ISO8601CalendarDateConverter : IsoDateTimeConverter
    {
        public ISO8601CalendarDateConverter()
        {
            base.DateTimeFormat = "yyyy'-'MM'-'dd";
        }
    }
}
