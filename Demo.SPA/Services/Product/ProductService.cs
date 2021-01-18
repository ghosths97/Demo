using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Json;
using Demo.Shared.Models.Domain;

namespace Demo.SPA.Services.Product
{
    public class ProductService : IProductService
    {
        private HttpClient _ProductHttpClient { get; }
        private JsonSerializerOptions op = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductService(HttpClient httpClient)
        {
            _ProductHttpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var response = await _ProductHttpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("/Product/All");            
            return response;
        }

        public async Task<ProductDto> GetProductAsync(int ProductId)
        {
            var response = await _ProductHttpClient.GetFromJsonAsync<ProductDto>($"/Product?productID={ProductId}");
            return response;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto Product)
        {
            var response = await _ProductHttpClient.PostAsJsonAsync<ProductDto>("/Product", Product);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProductDto>(json, op);
            return result;
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto Product)
        {
            var productJson = new Helpers.JsonContent(Product);
            var response = await _ProductHttpClient.PutAsync("/Product", productJson);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProductDto>(json, op);
            return result;
        }

        public async Task<bool> DeleteProductAsync(int ProductId)
        {
            var response = await _ProductHttpClient.DeleteAsync($"/Product?productID={ProductId}");
            return (response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        public async Task<IEnumerable<CompanyResponse>> GetAllCompaniesAsync()
        {
            var response = await _ProductHttpClient.GetFromJsonAsync<IEnumerable<CompanyResponse>>("/Company/All");
            return response;
        }
    }
}
