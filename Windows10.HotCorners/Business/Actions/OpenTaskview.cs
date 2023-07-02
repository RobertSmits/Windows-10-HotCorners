using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class OpenTaskView : IAction
{
    private readonly ILogWriter _logWriter;

    public OpenTaskView(ILogWriter logWriter)
    {
        _logWriter = logWriter;
    }

    public ActionType ActionType => ActionType.TaskView;
    public string GetHelp() => "Show Taskview";

    public void DoAction()
    {
        _logWriter.WriteLog<OpenTaskView>(LogLevel.Status, "Opening TaskView");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
        }
        catch (Exception e)
        {
            _logWriter.WriteLog<OpenTaskView>(LogLevel.Error, e.ToString());
        }
    }
}