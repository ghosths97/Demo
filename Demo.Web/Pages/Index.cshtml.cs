using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Demo.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Product> Products;

        public String pageName = "Products";

        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
           var client  =  _httpClientFactory.CreateClient("products");
         
           var result = await client.GetAsync("/Product/All");
           
          //  result.EnsureSuccessStatusCode();

            var json  = await result.Content.ReadAsStringAsync();

            Products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

        }

        public void OnPost()
        {

        }
    }
}
