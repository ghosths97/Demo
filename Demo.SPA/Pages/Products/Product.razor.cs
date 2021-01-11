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

        private Models.ProductDto product { get; set; }


        bool IsBusy { get; set; }

        protected async override Task OnInitializedAsync()
        {
            product = await productService.GetProduct(Id);
            await base.OnInitializedAsync();
        }

        private async Task Save()
        {
            IsBusy = true;
            await productService.UpdateProduct(product);            
            IsBusy = false;
        }

        private async Task Delete()
        {
            IsBusy = true;
            var deleted = await productService.DeleteProduct(product.id);
            if (deleted)
            {
                NavigationManager.NavigateTo("products");
            }
            IsBusy = false;
        }
    }
}
