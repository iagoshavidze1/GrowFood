using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GrowFood.Application.Queries.AccountQueries;
using GrowFood.Infrastructure.Data;
using GrowFood.Shared.Security;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GrowFood.Api
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
            services.AddControllers();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Grow Food Api", Version = "v1" });
            });

            var t = typeof(LoginQuery).GetTypeInfo().Assembly;

            services.AddMediatR(typeof(LoginQuery).GetTypeInfo().Assembly);

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<GrowFoodDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("GrowFoodDbContext")))
               .BuildServiceProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                ServerAutoMigration(app);
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrowFood Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ServerAutoMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<GrowFoodDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetService<GrowFoodDbContext>();
                //var passwordHasher = serviceScope.ServiceProvider.GetService<IPasswordHasher>();
                //Initializer.Seed(context, passwordHasher);
                context.SaveChanges();
            }
        }
    }
}
