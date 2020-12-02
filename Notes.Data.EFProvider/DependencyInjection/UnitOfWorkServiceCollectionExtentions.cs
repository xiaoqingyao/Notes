using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Data.EFProvider.DependencyInjection
{
    public static class UnitOfWorkServiceCollectionExtentions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, unitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, unitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, unitOfWork<TContext>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, unitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, unitOfWork<TContext2>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(
            this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, unitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, unitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, unitOfWork<TContext3>>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3, TContext4>(
            this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
            where TContext4 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, unitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, unitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, unitOfWork<TContext3>>();
            services.AddScoped<IUnitOfWork<TContext4>, unitOfWork<TContext4>>();

            return services;
        }
    }
}