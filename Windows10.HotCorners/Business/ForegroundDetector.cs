using System.Runtime.InteropServices;

namespace Windows10.HotCorners.Business;

internal class ForegroundDetect
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern IntPtr GetDesktopWindow();
    [DllImport("user32.dll")]
    private static extern IntPtr GetShellWindow();
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowRect(IntPtr hwnd, out Rect rc);

    public static bool CheckFullScreen()
    {
        var desktopHandle = GetDesktopWindow();
        var shellHandle = GetShellWindow();
        var hWnd = GetForegroundWindow();

        if (hWnd.Equals(IntPtr.Zero)) return false;
        if (hWnd.Equals(desktopHandle) || hWnd.Equals(shellHandle)) return false;
        _ = GetWindowRect(hWnd, out Rect appBounds);
        var screenBounds = Screen.FromHandle(hWnd).Bounds;
        return (appBounds.Bottom - appBounds.Top) == screenBounds.Height && (appBounds.Right - appBounds.Left) == screenBounds.Width;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
