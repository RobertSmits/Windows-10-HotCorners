using System;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions
{
    public class ShowDesktop : IAction
    {
        public ActionType ActionType => ActionType.Desktop;
        public string GetHelp() => "Show Desktop";

        public void DoAction()
        {
            Context.WriteLog<ShowDesktop>(LogLevel.Status, "Showing Desktop");
            var ks = new KeyboardSimulator(new InputSimulator());
            try
            {
                ks.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_D);
            }
            catch (Exception e)
            {
                Context.WriteLog<ShowDesktop>(LogLevel.Error, e.ToString());
            }
        }
    }
}