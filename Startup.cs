using System;
using AutoMapper;
using Github_backend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Hangfire.MySql;
using Hangfire;
using Github_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Github_backend
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

            var server =  Configuration["DbServer"] ;
            var port = Configuration["DbPort"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DbUser"];
            var password = Configuration["DbPassword"];

            System.Console.WriteLine($"server={server};port={port};database={database};user={user};password={password}");
            services.AddDbContext<ProjectContext>( options => 
            options.UseMySQL(
                $"server={server};port={port};database={database};user={user};password={password}"
            )
            );
            services.AddControllers().AddNewtonsoftJson(s => {
               s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
            })
            ;
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped(typeof(IGithubRepo<Project>),typeof(MySqlGithubRepo));
            
            


             services.AddHangfire(config => {
                config.UseStorage(
                    new MySqlStorage(
                        $"server={server};port={port};database={database};user={user};password={password};Allow User Variables=True",
                        new MySqlStorageOptions { TablesPrefix = "Hangfire"}
                    )
                );
            });
            services.AddHangfireServer();
            services.AddCors( options => {
                options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseHangfireDashboard();

            app.UseCors("AllowAll");
     

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<ProjectContext>();
        context.Database.Migrate();
    }
        }
    }
}
