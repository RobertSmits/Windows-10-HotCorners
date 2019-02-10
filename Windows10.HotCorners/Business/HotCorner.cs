using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Windows10.HotCorners.Business.Actions;
using Windows10.HotCorners.Extension;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Business
{
    internal class HotCorner
    {

        private readonly IConfiguration _configuration;
        private readonly IEnumerable<IAction> _actions;
        private RunState _state;
        private List<Corner> _corners;

        public HotCorner(IConfiguration configuration, IEnumerable<IAction> actions)
        {
            _configuration = configuration;
            _actions = actions;
            _state = RunState.Init;
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
            Context.WriteLog<HotCorner>(LogLevel.Status, "Loading screen configuration");

            _corners = new List<Corner>();
            if (_configuration.MultiMonitor)
                _corners = Screen.AllScreens.SelectMany(x => x.Bounds.GetCorners()).ToList();
            else
                _corners = Screen.PrimaryScreen.Bounds.GetCorners().ToList();

            Context.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.LeftTop));
            Context.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.RightTop));
            Context.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.LeftBottom));
            Context.WriteLog<HotCorner>(LogLevel.Status, _corners.Where(c => c.CornerType == CornerType.RightBottom));
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
                    Context.WriteLog<HotCorner>(LogLevel.Status, $"Hit {corner.CornerType}");
                    _actions.FirstOrDefault(a => a.ActionType == SelectAction(corner))?.DoAction();
                }
                Context.WriteLog<HotCorner>(LogLevel.Trace, pos);
            }

            while (Cursor.Position == pos) Thread.Sleep(100);
            Thread.Sleep(100);
        }

        private ActionType SelectAction(Corner corner)
        {
            switch (corner.CornerType)
            { 
                case CornerType.LeftTop: return _configuration.LeftTopAction;
                case CornerType.RightTop: return _configuration.RightTopAction;
                case CornerType.LeftBottom: return _configuration.LeftBottomAction;
                case CornerType.RightBottom: return _configuration.RightBottomAction;
                default: return ActionType.NoAction;
            }
        }

        private enum RunState
        {
            Init,
            Run,
            Error
        }
    }
}
