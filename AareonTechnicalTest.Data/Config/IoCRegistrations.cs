using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries;
using AareonTechnicalTest.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AareonTechnicalTest.Data.Config
{
    public static class IoCRegistrations
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(BuildUpdateableDbContext);
            services.AddScoped(BuildReadOnlyDbContext);

            return services;
        }

        private static IDbContext BuildUpdateableDbContext(IServiceProvider services)
        {
            return BuildDbContext(services, null);
        }

        private static IReadOnlyDbContext BuildReadOnlyDbContext(IServiceProvider services)
        {
            return (IReadOnlyDbContext)BuildDbContext(services, builder =>
           {
               builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
           });
        }

        private static ApplicationContext BuildDbContext(IServiceProvider services, Action<DbContextOptionsBuilder<ApplicationContext>> options)
        {
            var envDir = Environment.CurrentDirectory;

            var databasePath = $"{envDir}{System.IO.Path.DirectorySeparatorChar}Ticketing.db";

            var builder = new DbContextOptionsBuilder<ApplicationContext>();

            builder.UseSqlite($"Data Source={databasePath}");

            options?.Invoke(builder);

            return new ApplicationContext(builder.Options);
        }
    }
}