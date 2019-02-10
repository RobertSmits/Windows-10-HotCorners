using System;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions
{
    public class OpenStart : IAction
    {
        public ActionType ActionType => ActionType.Sart;
        public string GetHelp() => "Show Start";

        public void DoAction()
        {
            Context.WriteLog<OpenStart>(LogLevel.Status, "Opening Start");
            var ks = new KeyboardSimulator(new InputSimulator());
            try
            {
                ks.KeyPress(VirtualKeyCode.LWIN);
            }
            catch (Exception e)
            {
                Context.WriteLog<OpenStart>(LogLevel.Error, e.ToString());
            }
        }
    }
}