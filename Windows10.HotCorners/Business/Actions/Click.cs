using Microsoft.Extensions.Logging;
using WindowsInput;

namespace Windows10.HotCorners.Business.Actions;

internal class Click : IAction
{
    private readonly ILogger _logger;

    public Click(ILogger<Click> logger)
    {
        _logger = logger;
    }

    public ActionType ActionType => ActionType.Click;
    public string GetHelp() => "Click";

    public void DoAction()
    {
        _logger.LogInformation("Clicking");

        var m = new MouseSimulator(new InputSimulator());
        try
        {
            m.LeftButtonClick();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "error occured while executing action");
        }
    }
}