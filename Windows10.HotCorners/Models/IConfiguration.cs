using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Windows10.HotCorners.Business.Actions;

namespace Windows10.HotCorners.Models
{
    internal interface IConfiguration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        ActionType LeftTopAction { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        ActionType LeftBottomAction { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        ActionType RightTopAction { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        ActionType RightBottomAction { get; set; }

        bool MultiMonitor { get; set; }
        bool DisableOnFullScreen { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        LogLevel LogLevel { get; set; }
    }
}
