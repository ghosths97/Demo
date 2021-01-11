using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Demo.SPA.Services.Product;
using Blazored.LocalStorage;
using Demo.SPA.Services.Authentication;
using Demo.SPA.Util;

namespace Demo.SPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Urls.ProductsBase) });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
