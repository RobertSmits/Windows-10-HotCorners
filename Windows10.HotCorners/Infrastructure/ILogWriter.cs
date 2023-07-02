using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Infrastructure;

internal interface ILogWriter
{
    void WriteLog<T>(LogLevel logType, string log);
    void WriteLog<T>(LogLevel logType, object log);
}
