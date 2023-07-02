using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Windows10.HotCorners.Business.Actions;
using Windows10.HotCorners.Infrastructure;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Business;

internal static class Bootstrapper
{
    public static void ConfigureContainer(IServiceCollection container)
    {
        container.AddSingleton(GetConfiguration());
        container.AddSingleton<ILogWriter, LogWriter>();
        container.AddSingleton<HotCorner>();

        var actionTypes = Assembly.GetAssembly(typeof(Bootstrapper))
            .GetTypes()
            .Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IAction)));

        foreach (var actionType in actionTypes)
        {
            container.AddSingleton(typeof(IAction), actionType);
        }
    }

    private static IConfiguration GetConfiguration()
    {
        var configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var configFile = Path.Combine(configFilePath, "HotCorners.json");
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters = {
                new JsonStringEnumConverter()
            }
        };


        if (File.Exists(configFile))
        {
            var json = File.ReadAllText(configFile);
            return JsonSerializer.Deserialize<Configuration>(json, options) ?? new Configuration();
        }
        else
        {
            var config = new Configuration();
            File.WriteAllText(configFile, JsonSerializer.Serialize(config, options));
            return config;
        }
    }
}
