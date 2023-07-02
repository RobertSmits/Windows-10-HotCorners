﻿using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Windows10.HotCorners.Business.Actions;

internal class OpenStart : IAction
{
    private readonly ILogWriter _logWriter;

    public OpenStart(ILogWriter logWriter)
    {
        _logWriter = logWriter;
    }

    public ActionType ActionType => ActionType.Sart;
    public string GetHelp() => "Show Start";

    public void DoAction()
    {
        _logWriter.WriteLog<OpenStart>(LogLevel.Status, "Opening Start");
        var ks = new KeyboardSimulator(new InputSimulator());
        try
        {
            ks.KeyPress(VirtualKeyCode.LWIN);
        }
        catch (Exception e)
        {
            _logWriter.WriteLog<OpenStart>(LogLevel.Error, e.ToString());
        }
    }
}