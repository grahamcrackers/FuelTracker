using AutoMapper;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories.DependencyInjection;
using GasTracker.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace GasTracker.API
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
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // Add DB
            var connection = Configuration.GetConnectionString("PostgresConnection");
            services.AddDbContext<TrackerContext>(options => options.UseNpgsql(connection));
            services.AddUnitOfWork<TrackerContext>();
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IVehicleService, VehicleService>();
            // services.AddScoped<ITripService, TripService>();


            // AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                // mc.AllowNullCollections = true;
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton<IMapper>(mappingConfig.CreateMapper());

            // Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info { Title = "Gas Tracker API", Version = "v1" });
            });

            // CORS
            services.AddCors();

            // Health Check
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // else
            // {
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Gas Tracker API");
                config.RoutePrefix = string.Empty; // point root to swagger
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                       
            });

            app.UseHealthChecks("/health");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
