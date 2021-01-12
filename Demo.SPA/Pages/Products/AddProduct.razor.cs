using Demo.Shared.Security;
using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Products
{
    [Authorize(Policy = Permissions.AddProduct)]
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

        /// <summary>
        /// Save Product
        /// </summary>
        /// <returns></returns>
        private async Task Save()
        {
            IsBusy = true;
            await productService.AddProduct(product);
            IsBusy = false;
        }
    }
}
