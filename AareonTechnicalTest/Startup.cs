using System;
using System.Linq;
using System.Text.Json.Serialization;
using AareonTechnicalTest.Application.Config;
using AareonTechnicalTest.Data;
using AareonTechnicalTest.Data.Config;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.Interceptors;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AareonTechnicalTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(c => c.UseSqlite());
            services.AddControllers().AddFluentValidation(ConfigureFluentValidation).AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
            services.AddInfrastructure();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCqrsHandlers();
            services.AddTransient<IValidatorInterceptor, MergeModelFromRouteValidatorInterceptor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AareonTechnicalTest", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        private void ConfigureFluentValidation(FluentValidationMvcConfiguration config)
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly =>
                {
                    var name = assembly.GetName();
                    if (name.Name != null)
                    {
                        return name.Name.StartsWith("AareonTechnicalTest");
                    }

                    return false;
                });

            config.RegisterValidatorsFromAssemblies(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AareonTechnicalTest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}