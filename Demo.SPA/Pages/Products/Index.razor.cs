using Demo.Shared.Models.Domain;
using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Products
{
    [Route("products")]
    [Authorize]
    public partial class Index
    {
        [Inject]
        private IProductService _productService { get; set; }

        private IEnumerable<ProductDto> products;

        private bool loading { get; set; }

        public Index()
        {
            
        }

        protected override async Task OnInitializedAsync()
        {
            products = await _productService.GetAllProductsAsync();
        }
    }
}
