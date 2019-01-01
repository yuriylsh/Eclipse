using System;
using System.Collections;
using System.Linq;

namespace ClearCode
{
    public static class ConfigurationLoader
    {
        public static IClearCodeConfiguration LoadConfiguration(Func<string, object> getVariableValue)
        {
            var configuration = GetDefaultConfiguration();
            if (!(getVariableValue("ClearCodeConfiguration") is IDictionary configVariable)) 
                return configuration;

            if (configVariable.Contains(nameof(IClearCodeConfiguration.ToRemoveDirectories)))
            {
                configuration.ToRemoveDirectories =
                    ToStringArray(configVariable[nameof(IClearCodeConfiguration.ToRemoveDirectories)]);
            }

            if (configVariable.Contains(nameof(IClearCodeConfiguration.ToRemoveFiles)))
            {
                configuration.ToRemoveFiles =
                    ToStringArray(configVariable[nameof(IClearCodeConfiguration.ToRemoveFiles)]);
            }

            if (configVariable.Contains(nameof(IClearCodeConfiguration.Destinations)))
            {
                var destinationsArray = ToStringArray(configVariable[nameof(IClearCodeConfiguration.Destinations)]);
                configuration.Destinations = new IDestination[destinationsArray.Length];
                for (var i = 0; i < destinationsArray.Length; i++)
                {
                    configuration.Destinations[i] = ParseDestination(destinationsArray[i]);
                }
            }

            return configuration;
        }

        private static string[] ToStringArray(object objectArray)
            => ((object[]) objectArray).Cast<string>().ToArray();

        private static readonly char[] DestinationSeparator = {';'};
        private static IDestination ParseDestination(string destination)
        {
            var parts = destination.Split(DestinationSeparator);
            return parts.Length > 1
                ? new Destination { Label = parts[0], Path = parts[1] }
                : new Destination { Label = "[Please correct the value]", Path = "." };
        }

        private static ClearCodeConfiguration GetDefaultConfiguration() =>
            new ClearCodeConfiguration
            {
                ToRemoveDirectories = new[]
                {
                    "bin",
                    "obj",
                    "ClientApp\\node_modules"
                },
                ToRemoveFiles = new[] { "ClientApp\\package-lock.json" },
                Destinations = Array.Empty<IDestination>()
            };

        private class ClearCodeConfiguration : IClearCodeConfiguration
        {
            public string[] ToRemoveDirectories { get; set; }
            public string[] ToRemoveFiles { get; set; }
            public IDestination[] Destinations { get; set; }
        }

        private class Destination: IDestination
        {
            public string Label { get; set; }
            public string Path { get; set; }
            public override string ToString() => $"{Label} => {Path}";
        }
    }
}