using Windows10.HotCorners.Business.Actions;

namespace Windows10.HotCorners.Models
{
    internal class Configuration : IConfiguration
    {
        public ActionType LeftTopAction { get; set; }
        public ActionType LeftBottomAction { get; set; }
        public ActionType RightTopAction { get; set; }
        public ActionType RightBottomAction { get; set; }
        public bool MultiMonitor { get; set; }
        public bool DisableOnFullScreen { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Disable;
    }
}
