using Demo.SPA.Models;
using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Products
{
    [Route("products")]
    public partial class Index
    {
        [Inject]
        private IProductService _productService { get; set; }

        private IEnumerable<Demo.SPA.Models.ProductDto> products;

        private bool loading { get; set; }

        public Index()
        {
            
        }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            products = await _productService.GetAllProducts();
            loading = false;
        }
    }
}
