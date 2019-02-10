using System;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions
{
    public class OpenTaskView : IAction
    {
        public ActionType ActionType => ActionType.TaskView;
        public string GetHelp() => "Show Taskview";

        public void DoAction()
        {
            Context.WriteLog<OpenTaskView>(LogLevel.Status, "Opening TaskView");
            var ks = new KeyboardSimulator(new InputSimulator());
            try
            {
                ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
            }
            catch (Exception e)
            {
                Context.WriteLog<OpenTaskView>(LogLevel.Error, e.ToString());
            }
        }
    }
}