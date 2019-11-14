using IPInfoCommon;
using IPInfoCommon.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace IPInfoWebAPI
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

            //Add Service to allow querying IP information
            services.AddTransient<IIpInfoLookUp>(s => new IpLookUp(Configuration["WorkerServer"]));

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName(); 
                c.SwaggerDoc($"v{assemblyName.Version.ToString()}", 
                    new OpenApiInfo 
                    { 
                        Title = "IP Info Web API", 
                        Description= "This API provides information about an IP or domain using predefined external services.<br/>Default service: GeoIP<br/>Other serices: RDAP, Reverse DNS, and Ping",
                        Version = $"v{assemblyName.Version.ToString()}",
                        Contact = new OpenApiContact
                        {
                            Name = "Maurice",
                            Email = "maus.ntare@gmail.com"
                        }
                    });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assemblyName.Name}.XML"));
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}/swagger.json", 
                                  $"IP Info Web API V{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
