using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;

namespace Windows10.HotCorners.Business.Actions;

internal class Click : IAction
{
    private readonly ILogWriter _logWriter;

    public Click(ILogWriter logWriter)
    {
        _logWriter = logWriter;
    }

    public ActionType ActionType => ActionType.Click;
    public string GetHelp() => "Click";

    public void DoAction()
    {
        _logWriter.WriteLog<Click>(LogLevel.Status, "Clicking");

        var m = new MouseSimulator(new InputSimulator());
        try
        {
            m.LeftButtonClick();
        }
        catch (Exception e)
        {
            _logWriter.WriteLog<Click>(LogLevel.Error, e.ToString());
        }
    }
}