using Microsoft.Win32;
using Windows10.HotCorners.Business.Actions;
using Windows10.HotCorners.Extension;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Business;

internal class HotCorner
{
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<IAction> _actions;
    private readonly ILogWriter _logWriter;
    private RunState _state;
    private List<Corner> _corners;

    public HotCorner(IConfiguration configuration, IEnumerable<IAction> actions, ILogWriter logWriter)
    {
        _configuration = configuration;
        _actions = actions;
        _logWriter = logWriter;
        _state = RunState.Init;
        _corners = new List<Corner>();
        SystemEvents.DisplaySettingsChanged += (sender, args) => _state = RunState.Init;
    }

    public void Start()
    {
        while (true)
        {
            if (_state == RunState.Error)
                break;

            else if (_state == RunState.Init)
                Init();

            else if (_state == RunState.Run)
                Run();
        }
    }

    private void Init()
    {
        _logWriter.WriteLog<HotCorner>(LogLevel.Status, "Loading screen configuration");

        if (_configuration.MultiMonitor)
            _corners = Screen.AllScreens.SelectMany(x => x.Bounds.GetCorners()).ToList();
        else
            _corners = Screen.PrimaryScreen.Bounds.GetCorners().ToList();

        _logWriter.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.LeftTop));
        _logWriter.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.RightTop));
        _logWriter.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.LeftBottom));
        _logWriter.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.RightBottom));
        _state = RunState.Run;
    }

    private void Run()
    {
        var pos = Cursor.Position;
        if (!(_configuration.DisableOnFullScreen && ForegroundDetect.CheckFullScreen()))
        {
            var corner = _corners.FirstOrDefault(c => c.Point == pos);
            if (corner != null)
            {
                _logWriter.WriteLog<HotCorner>(LogLevel.Status, $"Hit {corner.CornerType}");
                _actions.FirstOrDefault(a => a.ActionType == SelectAction(corner))?.DoAction();
            }
            _logWriter.WriteLog<HotCorner>(LogLevel.Trace, pos);
        }

        while (Cursor.Position == pos) Thread.Sleep(100);
        Thread.Sleep(100);
    }

    private ActionType SelectAction(Corner corner)
    {
        return corner.CornerType switch
        {
            CornerType.LeftTop => _configuration.LeftTopAction,
            CornerType.RightTop => _configuration.RightTopAction,
            CornerType.LeftBottom => _configuration.LeftBottomAction,
            CornerType.RightBottom => _configuration.RightBottomAction,
            _ => ActionType.NoAction,
        };
    }

    private enum RunState
    {
        Init,
        Run,
        Error
    }
}
