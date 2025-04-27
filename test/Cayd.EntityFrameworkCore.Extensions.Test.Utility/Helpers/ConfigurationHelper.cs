using Cayd.EntityFrameworkCore.Extensions.Test.Utility.Fixtures;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Utility.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration? configuration;
        private static IConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(GetBaseDirectory())
                        .AddUserSecrets<TestDbContextFixture>();

                    configuration = builder.Build();
                }

                return configuration;
            }
        }

        private static string GetBaseDirectory([CallerFilePath] string? path = null)
        {
            return Path.GetDirectoryName(path)!;
        }

        public static T? GetOption<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }
    }
}
