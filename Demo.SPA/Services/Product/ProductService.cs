using Demo.SPA.Util;
using Demo.SPA.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace Demo.SPA.Services.Product
{
    public class ProductService : IProductService
    {
        private HttpClient _ProductHttpClient { get; }

        public ProductService(HttpClient httpClient)
        {
            _ProductHttpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var response = await _ProductHttpClient.GetJsonAsync<IEnumerable<ProductDto>>("/Product/All");
            return response;
        }

        public async Task<ProductDto> GetProduct(int ProductId)
        {
            var response = await _ProductHttpClient.GetJsonAsync<ProductDto>($"/Product?productID={ProductId}");
            return response;
        }

        public async Task<ProductDto> AddProduct(ProductDto Product)
        {
            var productJson = new JsonContent(Product);
            var response = await _ProductHttpClient.PostAsync("/Product", productJson);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProductDto>(json);
            return result;
        }

        public async Task<ProductDto> UpdateProduct(ProductDto Product)
        {
            var productJson = new JsonContent(Product);
            var response = await _ProductHttpClient.PutAsync("/Product", productJson);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProductDto>(json);
            return result;
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            var response = await _ProductHttpClient.DeleteAsync($"/Product?productID={ProductId}");
            return (response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
