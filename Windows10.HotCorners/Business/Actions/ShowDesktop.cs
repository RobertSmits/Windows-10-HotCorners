using Microsoft.Extensions.Logging;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class ShowDesktop : IAction
{
    private readonly ILogger _logger;

    public ShowDesktop(ILogger<ShowDesktop> logger)
    {
        _logger = logger;
    }

    public ActionType ActionType => ActionType.Desktop;
    public string GetHelp() => "Show Desktop";

    public void DoAction()
    {
        _logger.LogInformation("Showing Desktop");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_D);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "error occurred while executing action");
        }
    }
}