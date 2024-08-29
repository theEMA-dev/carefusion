using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Carefusion.Data;
using Carefusion.Data.Repositories;
using Carefusion.Business.Interfaces;
using Carefusion.Business.Services;
using DotNetEnv;
using Microsoft.OpenApi.Models;
using Carefusion.Data.Interfaces;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Carefusion.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Env.Load();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbString = Env.GetString("DB_STRING") ?? Environment.GetEnvironmentVariable("DB_STRING");

            if (string.IsNullOrEmpty(dbString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(dbString));

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IHospitalService, HospitalService>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            // Add Swagger generation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Carefusion API",
                    Description = "Healthcare API with .NET Entity Framework and Multi-Layer Architecture",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("ApiKeyAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-API-KEY",
                    Description = "API Key Authentication"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKeyAuth"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carefusion API v1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
