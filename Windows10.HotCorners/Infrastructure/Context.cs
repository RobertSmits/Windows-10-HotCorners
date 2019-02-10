using Newtonsoft.Json;
using System;
using Unity;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Infrastructure
{
    internal static class Context
    {
        public static UnityContainer Container { get; set; }

        public static void WriteLog<T>(LogLevel logType, string log)
        {
            var config = Container.Resolve<IConfiguration>();
            if (logType < config.LogLevel) return;
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            var text = $"{time}|{logType.ToString().ToUpper().PadRight(10)}|{typeof(T).Name.PadRight(14)}|   |{log}";
            Console.WriteLine(text);
        }
        public static void WriteLog<T>(LogLevel logType, object log)
        {
            var logString = JsonConvert.SerializeObject(log);
            WriteLog<T>(logType, logString);
        }
    }
}
