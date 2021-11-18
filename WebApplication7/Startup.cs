using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Enums;
using WebApplication7.Interfaces;
using WebApplication7.Middlewares;
using WebApplication7.Services;

namespace WebApplication7
{
    public class Startup
    {
        public delegate IParser ParserServiceResolver(ParserTypeEnum networkSource);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddApiVersioning(opt => opt.ReportApiVersions = true);
            services.AddSwaggerGen(swagger => {

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Parser",
                    Description = "Description ... ",
                    Version = "v1"
                });

                swagger.EnableAnnotations();
                swagger.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "WebApplication7.xml"));
            });


            services.AddSingleton(typeof(IAngleSharpManager), typeof(AngleSharpManager));
            services.AddSingleton(typeof(IHttpClientManager), typeof(HttpClientManager));
            services.AddSingleton(typeof(IFileParserService), typeof(FileParserService));

            services.AddTransient(typeof(ComfyrParserService));
            services.AddTransient(typeof(FoxtrotParserService));
            services.AddTransient(typeof(NotImplementedException));

            services.AddTransient<ParserServiceResolver>(serviceProvider => (networkSource) =>
            {
                switch (networkSource)
                {
                    case ParserTypeEnum.Comfy:
                        return serviceProvider.GetService<ComfyrParserService>();
                    case ParserTypeEnum.Hotline:
                        return serviceProvider.GetService<HotlineParserService>();
                    case ParserTypeEnum.Foxtrot:
                        return serviceProvider.GetService<FoxtrotParserService>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication7 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
