using Demo.SPA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.SPA.Services.Product
{
    public interface IProductService
    {
        Task<ProductDto> AddProduct(ProductDto Product);
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProduct(int ProductId);
        Task<ProductDto> UpdateProduct(ProductDto Product);
        Task<bool> DeleteProduct(int ProductId);
    }
}