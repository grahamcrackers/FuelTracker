using AutoMapper;
using GasTracker.Data.Models;
using GasTracker.Repositories.DependencyInjection;
using GasTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GasTracker
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var connection = Configuration.GetConnectionString("DefaultConnection");

            // Add DB
            services.AddDbContext<TrackerContext>(options => options.UseSqlite(connection));
            services.AddUnitOfWork<TrackerContext>();

            // Add Services
            services.AddScoped(typeof(IService<User>), typeof(UserService));
            services.AddScoped(typeof(IService<Vehicle>), typeof(VehicleService));
            // services.AddScoped(typeof(IService<Trip>), typeof(TripService));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton<IMapper>(mappingConfig.CreateMapper());

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info { Title = "Gas Tracker API", Version = "v1" });
            });

            services.AddCors();

            // Add a health check page
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
            });

            app.UseCors(builder => {
                builder.AllowAnyOrigin();
            });
            
            app.UseHealthChecks("/health");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
