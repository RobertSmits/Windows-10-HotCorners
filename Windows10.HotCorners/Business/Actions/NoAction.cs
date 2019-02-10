namespace Windows10.HotCorners.Business.Actions
{
    public class NoAction : IAction
    {
        public ActionType ActionType => ActionType.NoAction;
        public string GetHelp() => "No action";

        public void DoAction()
        {
        }
    }
}