using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class OpenExplorer : IAction
{
    private readonly ILogWriter _logWriter;

    public OpenExplorer(ILogWriter logWriter)
    {
        _logWriter = logWriter;
    }

    public ActionType ActionType => ActionType.Explorer;
    public string GetHelp() => "Open Explorer";

    public void DoAction()
    {
        _logWriter.WriteLog<OpenExplorer>(LogLevel.Status, "Opening Explorer");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
        }
        catch (Exception e)
        {
            _logWriter.WriteLog<OpenExplorer>(LogLevel.Error, e.ToString());
        }
    }
}