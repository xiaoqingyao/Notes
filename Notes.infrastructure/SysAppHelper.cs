using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Notes.infrastructure
{
    public static class SysAppHelper
    {
        public static IConfigurationBuilder AddJF(this IConfigurationBuilder builder, IHostEnvironment env, params string[] fileName)
        {

            foreach (var item in fileName)
            {

                builder.AddJsonFile($"{item}.json");
                builder.AddJsonFile($"{item}.{env.EnvironmentName}.json");
            }
            return builder;

        }
    }
}
