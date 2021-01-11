using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Products
{
    partial class AddProduct
    {
        [Inject]
        public IProductService productService { get; set; }

        private Models.ProductDto product { get; set; }

        bool IsBusy { get; set; }

        public AddProduct()
        {
            product = new Models.ProductDto();
        }

        private async Task Save()
        {
            IsBusy = true;
            await productService.AddProduct(product);
            IsBusy = false;
        }
    }
}
