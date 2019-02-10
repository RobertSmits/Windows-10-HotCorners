using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using Unity;
using Unity.RegistrationByConvention;
using Windows10.HotCorners.Business.Actions;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Business
{
    internal static class Bootstrapper
    {
        public static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterInstance<IConfiguration>(GetConfiguration());

            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies().Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IAction))),
                WithMappings.FromAllInterfaces,
                WithName.TypeName
            );
        }

        private static IConfiguration GetConfiguration()
        {
            var configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var configFile = Path.Combine(configFilePath, "HotCorners.json");

            if (File.Exists(configFile))
            {
                var json = File.ReadAllText(configFile);
                return JsonConvert.DeserializeObject<Configuration>(json);
            }
            else
            {
                var config = new Configuration();
                File.WriteAllText(configFile, JsonConvert.SerializeObject(config, Formatting.Indented));
                return config;
            }
        }
    }
}
