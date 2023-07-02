using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Windows10.HotCorners.Business.Actions;
using Windows10.HotCorners.Extension;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Business;

internal class HotCorner
{
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<IAction> _actions;
    private readonly ILogger _logger;
    private RunState _state;
    private List<Corner> _corners;

    public HotCorner(ILogger<HotCorner> logger, IConfiguration configuration, IEnumerable<IAction> actions)
    {
        _logger = logger;
        _configuration = configuration;
        _actions = actions;
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
        _logger.LogInformation("Loading screen configuration");

        if (_configuration.MultiMonitor)
            _corners = Screen.AllScreens.SelectMany(x => x.Bounds.GetCorners()).ToList();
        else
            _corners = Screen.PrimaryScreen.Bounds.GetCorners().ToList();

        _logger.LogDebug("Left Top:     {cornerList}", JsonSerializer.Serialize(_corners.Where(c => c.CornerType == CornerType.LeftTop)));
        _logger.LogDebug("Right Top:    {cornerList}", JsonSerializer.Serialize(_corners.Where(c => c.CornerType == CornerType.RightTop)));
        _logger.LogDebug("Left Bottom:  {cornerList}", JsonSerializer.Serialize(_corners.Where(c => c.CornerType == CornerType.LeftBottom)));
        _logger.LogDebug("Right Bottom: {cornerList}", JsonSerializer.Serialize(_corners.Where(c => c.CornerType == CornerType.RightBottom)));
        _state = RunState.Run;
    }

    private void Run()
    {
        var pos = Cursor.Position;
        if (!(_configuration.DisableOnFullScreen && ForegroundDetect.CheckFullScreen()))
        {
            _logger.LogTrace("Current Position: X: {x} Y: {y}", pos.X, pos.Y);
            var corner = _corners.FirstOrDefault(c => c.Point == pos);
            if (corner != null)
            {
                _logger.LogInformation("Hit {cornerType}:", corner.CornerType);
                _actions.FirstOrDefault(a => a.ActionType == SelectAction(corner))?.DoAction();
            }
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
