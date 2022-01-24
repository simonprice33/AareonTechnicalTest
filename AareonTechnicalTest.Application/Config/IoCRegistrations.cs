using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AareonTechnicalTest.Application.Config
{
    public static class IoCRegistrations
    {
        /// <summary>
        /// Method to add IoC registration.
        /// </summary>
        /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
        /// <returns>Returns an instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(IoCRegistrations));

            return services;
        }
    }
}