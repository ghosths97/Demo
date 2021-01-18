using Demo.Shared.Models.Domain;
using Demo.Shared.Security;
using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Products
{
    [Authorize(Policy = Policy.AddProduct)]
    partial class AddProduct
    {
        [Inject]
        public IProductService productService { get; set; }

        private ProductDto product { get; set; }

        bool IsBusy { get; set; }

        public AddProduct()
        {
            product = new ProductDto();
        }

        /// <summary>
        /// Save Product
        /// </summary>
        /// <returns></returns>
        private async Task Save()
        {
            IsBusy = true;
            await productService.AddProductAsync(product);
            IsBusy = false;
        }
    }
}
