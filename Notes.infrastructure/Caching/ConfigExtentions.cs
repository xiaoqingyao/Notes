using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Infrastructure.Serializer;

namespace Notes.Infrastructure.Caching
{
    public static class ConfigExtentions
    {

        public static IServiceCollection AddRedisCaching(this IServiceCollection services, IConfiguration config)
        {


            services.AddEasyCaching(ops =>
            {

                ops.UseInMemory(config, "cache");
                ops.UseRedis(config, "redis");


            });
            services.AddSingleton<ICachingProvider, CacheProvider>();
            services.AddSingleton<ISerializer, Serializer.Serializer>();


            return services;

        }

    }
}
