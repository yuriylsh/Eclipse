using System;
using System.Collections;

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
                    (string[]) configVariable[nameof(IClearCodeConfiguration.ToRemoveDirectories)];
            }

            if (configVariable.Contains(nameof(IClearCodeConfiguration.ToRemoveFiles)))
            {
                configuration.ToRemoveFiles =
                    (string[]) configVariable[nameof(IClearCodeConfiguration.ToRemoveFiles)];
            }

            return configuration;
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
                ToRemoveFiles = new[] { "ClientApp\\package-lock.json" }
            };

        private class ClearCodeConfiguration : IClearCodeConfiguration
        {
            public string[] ToRemoveDirectories { get; set; }
            public string[] ToRemoveFiles { get; set; }
        }
    }
}