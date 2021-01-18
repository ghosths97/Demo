using Demo.Shared.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.SPA.Services.Product
{
    public interface IProductService
    {
        Task<ProductDto> AddProductAsync(ProductDto Product);

        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        Task<ProductDto> GetProductAsync(int ProductId);

        Task<ProductDto> UpdateProductAsync(ProductDto Product);

        Task<bool> DeleteProductAsync(int ProductId);

        Task<IEnumerable<CompanyResponse>> GetAllCompaniesAsync();
    }
}