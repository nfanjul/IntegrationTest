﻿using Microsoft.Extensions.Configuration;
using System.IO;

namespace IntegrationTest.IT
{
    public class TestConfigurationBuilder
    {
        public IConfigurationRoot Build()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.testing.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
        }
    }

}
