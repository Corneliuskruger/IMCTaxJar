using Microsoft.Extensions.Configuration;
using System.IO;

namespace IMCTaxJar.Services
{
    public static class ConfigurationService
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationService()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSettings.json")
                    .Build();
        }
    }
}
