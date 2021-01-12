using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Demo.SPA.Services.Product;
using Blazored.LocalStorage;
using Demo.SPA.Services.Authentication;
using Demo.SPA.Helpers;
using Demo.SPA.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Demo.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Demo.Shared.Helpers;

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

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client => client.BaseAddress = new Uri(Urls.ProductsBase));            
            
            builder.Services.AddBlazoredLocalStorage();

            //builder.Services.AddAuthorizationCore();
            builder.Services.AddAuthorizationWithPermissions();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
