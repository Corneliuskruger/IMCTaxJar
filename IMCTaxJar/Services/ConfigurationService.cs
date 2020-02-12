using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
