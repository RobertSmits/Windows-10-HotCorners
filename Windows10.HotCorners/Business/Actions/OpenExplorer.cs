using System;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions
{
    public class OpenExplorer : IAction
    {
        public ActionType ActionType => ActionType.Explorer;
        public string GetHelp() => "Open Explorer";

        public void DoAction()
        {
            Context.WriteLog<OpenExplorer>(LogLevel.Status, "Opening Explorer");
            var ks = new KeyboardSimulator(new InputSimulator());
            try
            {
                ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
            }
            catch (Exception e)
            {
                Context.WriteLog<OpenExplorer>(LogLevel.Error, e.ToString());
            }
        }
    }
}