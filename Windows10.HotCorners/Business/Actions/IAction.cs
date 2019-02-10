namespace Windows10.HotCorners.Business.Actions
{
    public interface IAction
    {
        ActionType ActionType { get; }
        string GetHelp();
        void DoAction();
    }
}