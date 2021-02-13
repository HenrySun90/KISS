using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Kiss.Services;
using Kiss.WebApi.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kiss.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<KissContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("KissContext")));

            services.AddAuthentication(options =>
            {
                options.AddScheme<KissAuthHandler>(KissAuthHandler.SchemeName, "default scheme");
                options.DefaultAuthenticateScheme = KissAuthHandler.SchemeName;
                options.DefaultChallengeScheme = KissAuthHandler.SchemeName;
            });
        }

        // ÉèÖÃAutoFac·þÎñÈÝÆ÷
        public void ConfigureContainer(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.Load("Kiss.Services");
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Service")).AsSelf();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
