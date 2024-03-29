﻿using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Windows10.HotCorners.Business;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners;

internal class Program
{
    [DllImport("kernel32.dll")]
    private static extern bool AttachConsole(int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]

    static extern bool AllocConsole();

    static void Main()
    {
        ProcessChecker.CheckRunningProcess();
        var services = new ServiceCollection();
        Bootstrapper.ConfigureContainer(services);
        var serviceProvider = services.BuildServiceProvider();
        var minLogLevel = serviceProvider.GetRequiredService<IConfiguration>().LogLevel;

        if (minLogLevel < LogLevel.None)
            AllocConsole();
            
        serviceProvider.GetRequiredService<HotCorner>().Start();
    }
}
