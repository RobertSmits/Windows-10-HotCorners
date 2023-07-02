using Microsoft.Extensions.Logging;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class OpenExplorer : IAction
{
    private readonly ILogger logger;

    public OpenExplorer(ILogger<OpenExplorer> logger)
    {
        this.logger = logger;
    }

    public ActionType ActionType => ActionType.Explorer;
    public string GetHelp() => "Open Explorer";

    public void DoAction()
    {
        logger.LogInformation("Opening Explorer");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
        }
        catch (Exception e)
        {
            logger.LogError(e, "error occured while executing action");
        }
    }
}