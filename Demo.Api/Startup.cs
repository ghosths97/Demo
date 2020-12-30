using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Config;
using Demo.Filters;
using Demo.Middlewares;
using Demo.Persistence;
using Demo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Demo
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
            //services.Add((new ServiceDescriptor(typeof(GuidService), typeof(GuidService), ServiceLifetime.Singleton));
            //var test = Configuration.GetValue<bool>("Test");
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));

            services.AddTransient<IGuidService, IntService>();
            services.AddTransient<TestService, TestService>();
            services.AddSingleton("Global");
            //services.AddSingleton<IProductService, ProductService>();


            services.AddControllers(Config=>
            {
                Config.Filters.Add(typeof(CustomFilterAttribute));
                Config.Filters.Add(typeof(DemoExceptionHandlerFilterAttribute));
            });

            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config=> {

                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a_very_long_key_to_encrypt")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<DemoDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DemoConnection")); });

            services.AddScoped<IProductService, SqlProductService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Use();           
            //app.Run(ctx=>ctx.Response.WriteAsync("hello world"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

           

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMyMiddleware();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 

            
        }
    }
}
