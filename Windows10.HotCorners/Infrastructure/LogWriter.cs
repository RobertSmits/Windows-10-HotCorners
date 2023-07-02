using System.Text.Json;
using Unity;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Infrastructure;

internal class LogWriter : ILogWriter
{
    private readonly IConfiguration _configuration;

    public LogWriter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void WriteLog<T>(LogLevel logType, string log)
    {
        if (logType < _configuration.LogLevel) return;
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        var text = $"{time}|{logType.ToString().ToUpper(),-10}|{typeof(T).Name,-14}|   |{log}";
        Console.WriteLine(text);
    }

    public void WriteLog<T>(LogLevel logType, object log)
    {
        var logString = JsonSerializer.Serialize(log);
        WriteLog<T>(logType, logString);
    }
}
