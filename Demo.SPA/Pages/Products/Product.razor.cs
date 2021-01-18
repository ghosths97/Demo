using Demo.Shared.Models.Domain;
using Demo.SPA.Components;
using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Products
{
    [Route("products/product")]
    partial class Product
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductService productService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private ProductDto product { get; set; }

        bool IsBusy { get; set; }

        protected async override Task OnInitializedAsync()
        {
            product = await productService.GetProductAsync(Id);
            await base.OnInitializedAsync();
        }        
    }
}
