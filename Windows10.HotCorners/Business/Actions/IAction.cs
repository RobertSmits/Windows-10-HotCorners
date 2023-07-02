namespace Windows10.HotCorners.Business.Actions;

internal interface IAction
{
    ActionType ActionType { get; }
    string GetHelp();
    void DoAction();
}