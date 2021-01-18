using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Demo.Shared.Extensions;
using Demo.Shared.Helpers;
using Demo.SPA.Helpers;
using Demo.SPA.Providers;
using Demo.SPA.Services.Authentication;
using Demo.SPA.Services.Product;
using Demo.SPA.Services.User;
using Demo.SPA.Services.Security;
using Blazored.Toast;
using Demo.SPA.Components;

namespace Demo.SPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<TokenAuthrorizationHandler>();
            builder.Services.AddHttpClient<IProductService, ProductService>(client => client.BaseAddress = new Uri(Urls.ProductsBase))
                .AddHttpMessageHandler<TokenAuthrorizationHandler>();
            builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(Urls.ProductsBase))
                .AddHttpMessageHandler<TokenAuthrorizationHandler>();
            builder.Services.AddHttpClient<ISecurityService, SecurityService>(client => client.BaseAddress = new Uri(Urls.ProductsBase))
                .AddHttpMessageHandler<TokenAuthrorizationHandler>();

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client => client.BaseAddress = new Uri(Urls.ProductsBase));

            builder.Services.AddSingleton<Spinner>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();

            // builder.Services.AddAuthorizationCore();
            builder.Services.AddAuthorizationWithPermissions();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
