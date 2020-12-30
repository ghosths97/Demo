using Demo.Models;
using System.Collections.Generic;

namespace Demo.Services
{
    public interface IProductService
    {
        void AddProduct(Product p);
        Product GetProduct(int id);
        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Delete(int id);
    }
}