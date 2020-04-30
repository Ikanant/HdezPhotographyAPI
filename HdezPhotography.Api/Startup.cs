using AutoMapper;
using HdezPhotography.Api.DbContexts;
using HdezPhotography.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;

namespace HdezPhotography.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true; // <<< This will return a 406 NOT ACCEPTABLE if requested format is not allowed
            })
                .AddNewtonsoftJson(setupAction => setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()) // Allow us to use JSON.Net in my app. Only quick change is the contract resolver so properties are always camel cased
                .AddXmlDataContractSerializerFormatters(); // <<< Will now make XML a supported response message back to the client
                

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

             services.AddScoped<IPhotoLibraryRepository, PhotoLibraryRepository>();

            services.AddDbContext<PhotographyApiContext>(options => {
                options.UseSqlServer(
                    @"Server=127.0.0.1,1433;Database=PhotographyApiDB;User=SA;Password=HelloWorld10;");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler(appBuilder => {
                    appBuilder.Run(async context => {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Custom error msg: Something went wrong on the server. Sorry");
                    });
                });
            }

            app.UseRouting(); 

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers() );
        }
    }
}
