using Microsoft.Extensions.Logging;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class OpenTaskView : IAction
{
    private readonly ILogger _logger;

    public OpenTaskView(ILogger<OpenTaskView> logger)
    {
        _logger = logger;
    }

    public ActionType ActionType => ActionType.TaskView;
    public string GetHelp() => "Show Taskview";

    public void DoAction()
    {
        _logger.LogInformation("Opening TaskView");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "error occured while executing action");
        }
    }
}