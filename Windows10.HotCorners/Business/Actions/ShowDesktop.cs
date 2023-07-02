using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class ShowDesktop : IAction
{
    private readonly ILogWriter _logWriter;

    public ShowDesktop(ILogWriter logWriter)
    {
        _logWriter = logWriter;
    }

    public ActionType ActionType => ActionType.Desktop;
    public string GetHelp() => "Show Desktop";

    public void DoAction()
    {
        _logWriter.WriteLog<ShowDesktop>(LogLevel.Status, "Showing Desktop");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_D);
        }
        catch (Exception e)
        {
            _logWriter.WriteLog<ShowDesktop>(LogLevel.Error, e.ToString());
        }
    }
}