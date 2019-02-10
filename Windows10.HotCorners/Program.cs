using System.Runtime.InteropServices;
using Unity;
using Windows10.HotCorners.Business;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool AllocConsole();

        static void Main(string[] args)
        {
            ProcessChecker.CheckRunningProcess();
            var container = new UnityContainer();
            Bootstrapper.ConfigureContainer(container);
            Context.Container = container;

            if (container.Resolve<IConfiguration>().LogLevel != LogLevel.Disable)
                AllocConsole();

            Context.Container.Resolve<HotCorner>().Start();
        }
    }
}
