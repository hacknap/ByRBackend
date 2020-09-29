using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ByR.Controllers;
using ByR.Helpers;
using ByR.Entities;
using ByR.Data.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace ByR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public string DbConfig = "SqlDB";

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });


            services.AddScoped<UsersController>();

            services.AddCors(options => {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin().
                    AllowAnyHeader().
                    AllowAnyMethod();
                });
            });
            services.AddDbContext<DataContext>(options => options.UseSqlServer
            (Configuration.GetConnectionString(DbConfig)));


            //Data base repositories
            services.AddScoped<IProperty, PropertyRepository>();
            services.AddScoped<IUser, UserRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction())
            {
                DbConfig = "SqlDBRemoto";

            }
            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
