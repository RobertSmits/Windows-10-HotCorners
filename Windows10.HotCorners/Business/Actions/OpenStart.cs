using Microsoft.Extensions.Logging;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class OpenStart : IAction
{
    private readonly ILogger _logger;

    public OpenStart(ILogger<OpenStart> logger)
    {
        _logger = logger;
    }

    public ActionType ActionType => ActionType.Sart;
    public string GetHelp() => "Show Start";

    public void DoAction()
    {
        _logger.LogInformation("Opening Start");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.KeyPress(VirtualKeyCode.LWIN);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "error occured while executing action");
        }
    }
}