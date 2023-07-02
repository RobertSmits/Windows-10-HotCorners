using Windows10.HotCorners.Business.Actions;

namespace Windows10.HotCorners.Models;

internal interface IConfiguration
{
    ActionType LeftTopAction { get; set; }

    ActionType LeftBottomAction { get; set; }

    ActionType RightTopAction { get; set; }

    ActionType RightBottomAction { get; set; }

    bool MultiMonitor { get; set; }

    bool DisableOnFullScreen { get; set; }

    LogLevel LogLevel { get; set; }
}
