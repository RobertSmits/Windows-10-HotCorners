﻿using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Windows10.HotCorners.Business.Actions;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Business;

internal static class Bootstrapper
{
    public static void ConfigureContainer(IServiceCollection services)
    {
        var configuration = GetConfiguration();
        services.AddSingleton(configuration);
        services.AddSingleton<HotCorner>();

        var minLogLevel = configuration.LogLevel;
        services.AddLogging(builder => {
            builder
                .SetMinimumLevel(minLogLevel)
                .AddConsole();
        });

        var actionTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IAction)));

        foreach (var actionType in actionTypes)
        {
            services.AddSingleton(typeof(IAction), actionType);
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
