using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using oceny5._0.Entities;
using oceny5._0.Middleware;
using oceny5._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using oceny5._0.Models;

namespace oceny5._0
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
            var authenticaitonSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticaitonSettings);

            services.AddSingleton(authenticaitonSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticaitonSettings.JwtIssuer,
                    ValidAudience = authenticaitonSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticaitonSettings.JwtKey))
                };
            });



            services.AddControllers();
            services.AddDbContext<OcenyDBContext>();
            services.AddScoped<WykladowcaSeeder>();
            services.AddScoped<GrupaSeeder>();
            services.AddScoped<PrzedmiotSeeder>();
            services.AddScoped<StudentSeeder>();
            services.AddScoped<OcenaSeeder>();

            services.AddControllers().AddFluentValidation();

            services.AddScoped<IGrupaService, GrupaService>();
            services.AddScoped<IOcenyService, OcenyService>();
            services.AddScoped<IWykladowcaService, WykladowcaService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IValidator<CreateWykladowcaDto>,CreateWykladowcaDtoValidator>();

            services.AddSwaggerGen();

            services.AddScoped<IPasswordHasher<Wykladowca>, PasswordHasher<Wykladowca>>();

            services.AddAutoMapper(this.GetType().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            WykladowcaSeeder seeder, GrupaSeeder seeder1, PrzedmiotSeeder seeder2,
            StudentSeeder seeder3, OcenaSeeder seeder4)
        {
            seeder.Seed();
            seeder1.Seed();
            seeder2.Seed();
            seeder3.Seed();
            seeder4.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oceny API");
            });

            app.UseRouting();

           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
