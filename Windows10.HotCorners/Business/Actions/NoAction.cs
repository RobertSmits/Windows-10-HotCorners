namespace Windows10.HotCorners.Business.Actions;

internal class NoAction : IAction
{
    public ActionType ActionType => ActionType.NoAction;
    public string GetHelp() => "No action";

    public void DoAction()
    {
    }
}