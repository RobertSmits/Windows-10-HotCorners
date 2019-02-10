using System;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;

namespace Windows10.HotCorners.Business.Actions
{
    public class Click : IAction
    {
        public ActionType ActionType => ActionType.Click;
        public string GetHelp() => "Click";

        public void DoAction()
        {
            Context.WriteLog<Click>(LogLevel.Status, "Clicking");

            var m = new MouseSimulator(new InputSimulator());
            try
            {
                m.LeftButtonClick();
            }
            catch (Exception e)
            {
                Context.WriteLog<Click>(LogLevel.Error, e.ToString());
            }
        }
    }
}