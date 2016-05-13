using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using FlugDemo.Data;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using FlugDemo.Components;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace FlugDemo
{
    // ASP.NET Core 1
    public class Startup
    {
        IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            #region AddUserSecreta
            if (env.IsDevelopment())
                {
                    // dnu commands install Microsoft.Extensions.SecretManager
                    // user-secret set [key] [value]

                    builder.AddUserSecrets();
                }
            #endregion

            Configuration = builder.Build();

            var secureQueryString = Configuration["SecureConnectionString"];
            Debug.WriteLine(secureQueryString);

        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen();

            services.AddSignalR();

            services.AddMvc().AddMvcOptions(config => {

                var xmlInput = new XmlDataContractSerializerInputFormatter();
                var xmlOutput = new XmlDataContractSerializerOutputFormatter();

                config.InputFormatters.Add(xmlInput);
                config.OutputFormatters.Add(xmlOutput);

                config.OutputFormatters.Add(new CsvOutputFormatter());

            }).AddJsonOptions(settings => {
                settings.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                // Id ==> id
                // AblugOrt ==> ablugOrt
                settings.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddTransient<IFlugRepository, FlugEfRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(LogLevel.Debug);

            app.UseIISPlatformHandler();

            #region AddJwt
            
            app.UseJwtBearerAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.Authority = "https://steyer-identity-server.azurewebsites.net/identity";
                         // Schlüssel
                options.Audience = "https://steyer-identity-server.azurewebsites.net/identity/resources";
            });
            
            #endregion


            var origins = new[] { "http://localhost:8887" };
            app.UseCors(config => config.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/home/error");
            }

            app.UseStaticFiles();

            app.UseSignalR();

            app.UseSwaggerGen();
            app.UseSwaggerUi();

            app.UseMvcWithDefaultRoute();
                // /controller/methode
                // /flug/getAll
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
