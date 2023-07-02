using System.Diagnostics;

namespace Windows10.HotCorners.Business;

internal static class ProcessChecker
{
    public static void CheckRunningProcess()
    {
        var currentProcess = Process.GetCurrentProcess();
        var filename = Path.GetFileNameWithoutExtension(currentProcess.MainModule?.FileName);
        var process = Process.GetProcessesByName(filename).Where(p => p.Id != currentProcess.Id).ToArray();
        if (process.Length == 0) return;

        foreach (var p in process)
            p.Kill();

        Environment.Exit(0);
    }
}
