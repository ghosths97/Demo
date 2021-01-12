using System.Text;
using Demo.Api.Models.Identity;
using Demo.Configurations;
using Demo.Filters;
using Demo.Middlewares;
using Demo.Persistence;
using Demo.Services;
using Demo.Services.Roles;
using Demo.Shared.Extensions;
using Demo.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.Configure<EmailConfiguration>(Configuration.GetSection("Email"));

            services.AddTransient<IGuidService, IntService>();

            services.AddSingleton("Global");
            //services.AddSingleton<IProductService, ProductService>();

            services.AddCors();

            services.AddControllers(Config=>
            {
                Config.Filters.Add(typeof(CustomFilterAttribute));
                Config.Filters.Add(typeof(DemoExceptionHandlerFilterAttribute));
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config=> {

                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a_very_long_key_to_encrypt")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // permission policy
            services.AddAuthorizationWithPermissions();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddDbContext<DemoDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DemoConnection")); });
            // Identity
            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DemoDbContext>();

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

            app.UseCors(configurePolicy =>
                configurePolicy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

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
