using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Demo.Shared.Models.Domain;
using Demo.SPA.Services.Product;
using Microsoft.AspNetCore.Components;

namespace Demo.SPA.Components
{
    partial class ProductDetail
    {
        [Parameter]
        public ProductDto product { get; set; }

        [Inject]
        public Services.Product.IProductService productService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService toastService { get; set; }

        private IEnumerable<CompanyResponse> Companies;

        private Modal modal;

        protected async override Task OnInitializedAsync()
        {
            Companies = await productService.GetAllCompaniesAsync();
        }

        /// <summary>
        /// Save Product
        /// </summary>
        /// <returns></returns>
        private async Task Save()
        {
            Console.WriteLine(product.CompanyId);
            if (product.Id > 0)
            {
                await productService.UpdateProductAsync(product);
                toastService.ShowInfo("Product Updated Successfully.");
            }
            else
            {
                var _product = await productService.AddProductAsync(product);
                NavigationManager.NavigateTo($"products/detail/{_product.Id}");
                toastService.ShowSuccess("Product Added Successfully.");
            }
        }

        private async Task Delete()
        {
            var deleted = await productService.DeleteProductAsync(product.Id);
            if (deleted)
            {
                toastService.ShowWarning("Product Deleted Successfully.");
                NavigationManager.NavigateTo("products");
            }
        }
    }
}
